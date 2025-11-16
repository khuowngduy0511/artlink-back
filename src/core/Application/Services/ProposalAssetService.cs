using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using static Application.Commons.VietnameseEnum;

namespace Application.Services;

public class ProposalAssetService : IProposalAssetService
{
    private static readonly string PARENT_FOLDER = "ProposalAsset";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IClaimService _claimService;
    private readonly IMilestoneService _milstoneService;


    public ProposalAssetService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICloudinaryService cloudinaryService,
        IMilestoneService milstoneService,
        IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _milstoneService = milstoneService;
        _claimService = claimService;
    }

    public async Task<ProposalAssetVM> AddProposalAssetAsync(ProposalAssetModel proposalAssetModel)
    {
        // kiem tra xem proposal co ton tai khong
        var proposal = await _unitOfWork.ProposalRepository.GetByIdAsync(proposalAssetModel.ProposalId)
            ?? throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");

        // user phai gui tuan tu concept -> final -> revision
        //var proposalAssets = await _unitOfWork.ProposalAssetRepository.GetProposalAssetsOfProposalAsync(proposalAssetModel.ProposalId);
        //switch (proposalAssetModel.Type)
        //{
        //    case ProposalAssetEnum.Final:
        //        if (!proposalAssets.Any(x => x.Type == ProposalAssetEnum.Concept))
        //        {
        //            throw new BadHttpRequestException("Must send concept before sending final.");
        //        }
        //        break;
        //    case ProposalAssetEnum.Revision:
        //        if (!proposalAssets.Any(x => x.Type == ProposalAssetEnum.Final))
        //        {
        //            throw new BadHttpRequestException("Must send final before sending revision.");
        //        }
        //        break;
        //}

        // dat lai ten file
        string newProposalAssetName = $"{Path.GetFileNameWithoutExtension(proposalAssetModel.File.FileName)}_{DateTime.Now.Ticks}";
        string folderName = PARENT_FOLDER;
        string fileExtension = Path.GetExtension(proposalAssetModel.File.FileName);

        // Upload file lên Cloudinary
        var url = await _cloudinaryService.UploadFileAsync(proposalAssetModel.File, newProposalAssetName, folderName, false)
            ?? throw new KeyNotFoundException("Lỗi khi tải tài nguyên thỏa thuận lên Cloudinary.");

        // map assetModel sang proposalAsset
        ProposalAsset proposalAsset = _mapper.Map<ProposalAsset>(proposalAssetModel);
        proposalAsset.Location = url;
        proposalAsset.ProposalAssetName = newProposalAssetName + fileExtension;
        proposalAsset.ContentType = fileExtension.Replace(".", "");
        proposalAsset.Size = (ulong)proposalAssetModel.File.Length;
        await _unitOfWork.ProposalAssetRepository.AddAsync(proposalAsset);

        if (proposalAssetModel.Type == ProposalAssetEnum.Final)
        {
            proposal.ProposalStatus = ProposalStateEnum.Completed;
            _unitOfWork.ProposalRepository.Update(proposal);
        }

        // create proposal successfully -> add Init milestone
        await _milstoneService.AddMilestoneToProposalAsync(proposalAsset.ProposalId,
            $"Gửi tài nguyên thỏa thuận ({PROPOSALASSET_ENUM_VN[proposalAsset.Type]})");

        await _unitOfWork.SaveChangesAsync();

        var proposalAssetVM = _mapper.Map<ProposalAssetVM>(proposalAsset);
        return proposalAssetVM;
    }

    public async Task<string?> GetDownloadUriProposalAssetAsync(Guid proposalAssetId)
    {
        Guid loginId = _claimService.GetCurrentUserId ?? default!;

        var proposalAsset = await _unitOfWork.ProposalAssetRepository.GetProposalAssetsWithProposalAsync(proposalAssetId)
            ?? throw new KeyNotFoundException("Không tìm thấy tài nguyên thỏa thuận.");
        
        // Kiểm tra quyền truy cập
        if (_claimService.IsModeratorOrAdmin())
        {
            return proposalAsset.Location; // Return Cloudinary URL
        }

        // kiem tra xem user da mua asset chua
        if (proposalAsset.Proposal.CreatedBy!.Value != loginId
            && proposalAsset.Proposal.OrdererId != loginId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền tải tài nguyên này.");
        }

        if (proposalAsset.Proposal.OrdererId == loginId &&
            proposalAsset.Type == ProposalAssetEnum.Final &&
            proposalAsset.Proposal.ProposalStatus != ProposalStateEnum.CompletePayment)
        {
            throw new UnauthorizedAccessException("Phải hoàn tất thanh toán trước khi tải.");
        }

        // Return Cloudinary URL (file được upload với isPublic = false nên cần signed URL nếu cần)
        return proposalAsset.Location;
    }

    /// <summary>
    /// Download proposal asset dưới dạng stream - Backend làm proxy để bảo mật
    /// </summary>
    public async Task<(Stream stream, string fileName, string contentType)> DownloadProposalAssetAsync(Guid proposalAssetId)
    {
        Guid loginId = _claimService.GetCurrentUserId ?? default!;

        var proposalAsset = await _unitOfWork.ProposalAssetRepository.GetProposalAssetsWithProposalAsync(proposalAssetId)
            ?? throw new KeyNotFoundException("Không tìm thấy tài nguyên thỏa thuận.");

        // Kiểm tra quyền truy cập (giống GetDownloadUriProposalAssetAsync)
        if (!_claimService.IsModeratorOrAdmin())
        {
            if (proposalAsset.Proposal.CreatedBy!.Value != loginId
                && proposalAsset.Proposal.OrdererId != loginId)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền tải tài nguyên này.");
            }

            if (proposalAsset.Proposal.OrdererId == loginId &&
                proposalAsset.Type == ProposalAssetEnum.Final &&
                proposalAsset.Proposal.ProposalStatus != ProposalStateEnum.CompletePayment)
            {
                throw new UnauthorizedAccessException("Phải hoàn tất thanh toán trước khi tải.");
            }
        }

        // Download file từ Cloudinary qua backend (bảo mật)
        var stream = await _cloudinaryService.DownloadFileAsync(proposalAsset.Location);
        
        // Trả về stream + thông tin file
        var fileName = proposalAsset.ProposalAssetName;
        var contentType = GetContentType(proposalAsset.ContentType);
        
        return (stream, fileName, contentType);
    }

    /// <summary>
    /// Helper để lấy MIME type từ file extension
    /// </summary>
    private string GetContentType(string extension)
    {
        extension = extension.ToLower().TrimStart('.');
        return extension switch
        {
            "jpg" or "jpeg" => "image/jpeg",
            "png" => "image/png",
            "gif" => "image/gif",
            "bmp" => "image/bmp",
            "webp" => "image/webp",
            "pdf" => "application/pdf",
            "zip" => "application/zip",
            "rar" => "application/x-rar-compressed",
            "7z" => "application/x-7z-compressed",
            "psd" => "application/octet-stream",
            "ai" => "application/postscript",
            "mp4" => "video/mp4",
            "mov" => "video/quicktime",
            "avi" => "video/x-msvideo",
            _ => "application/octet-stream"
        };
    }

    public async Task<List<ProposalAssetVM>> GetProposalAssetsOfProposalAsync(Guid proposalId)
    {
        bool isProposalExisted = await _unitOfWork.ProposalRepository.IsExistedAsync(proposalId);
        if (!isProposalExisted)
        {
            throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");
        }

        var listProposalAsset = await _unitOfWork.ProposalAssetRepository
            .GetProposalAssetsOfProposalAsync(proposalId);
        var listProposalAssetVM = _mapper.Map<List<ProposalAssetVM>>(listProposalAsset);
        return listProposalAssetVM;
    }
}
