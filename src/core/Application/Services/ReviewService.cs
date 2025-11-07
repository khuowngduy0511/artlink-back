using Application.Commons;
using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using static Application.Commons.VietnameseEnum;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public ReviewService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IClaimService claimService
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
    }

    public async Task<PagedList<ReviewVM>> GetReviewsAsync(ReviewCriteria criteria)
    {
        var listReview = await _unitOfWork.ReviewRepository.GetReviewsAsync(
            criteria.Keyword, criteria.SortColumn, criteria.SortOrder, criteria.PageNumber, criteria.PageSize);
        return _mapper.Map<PagedList<ReviewVM>>(listReview);
    }

    public async Task<PagedList<ReviewVM>> GetReviewsByAccountIdAsync(Guid accountId, PagedCriteria criteria)
    {
        bool isAccountExisted = await _unitOfWork.AccountRepository.IsExistedAsync(accountId);
        if (!isAccountExisted)
        {
            throw new KeyNotFoundException("Không tìm thấy tài khoản.");
        }
        var reviews = await _unitOfWork.ReviewRepository.GetReviewsByAccountIdAsync(accountId, criteria.PageNumber, criteria.PageSize);
        return _mapper.Map<PagedList<ReviewVM>>(reviews);
    }

    public async Task<PagedList<ReviewVM>> GetReviewsByServiceIdAsync(Guid serviceId, PagedCriteria criteria)
    {
        bool isServiceExisted = await _unitOfWork.ServiceRepository.IsExistedAsync(serviceId);
        if (!isServiceExisted)
        {
            throw new KeyNotFoundException("Không tìm thấy dịch vụ.");
        }

        var reviews = await _unitOfWork.ReviewRepository.GetReviewsByServiceIdAsync(serviceId, criteria.PageNumber, criteria.PageSize);
        return _mapper.Map<PagedList<ReviewVM>>(reviews);
    }

    public async Task<ReviewVM> GetReviewByProposalIdAsync(Guid proposalId)
    {
        bool isProposalExisted = await _unitOfWork.ProposalRepository.IsExistedAsync(proposalId);
        if (!isProposalExisted)
        {
            throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");
        }
        var review = await _unitOfWork.ReviewRepository.GetReviewByProposalIdAsync(proposalId);
        var reviewVM = _mapper.Map<ReviewVM>(review);
        return reviewVM;
    }

    public async Task<ReviewVM> GetReviewAsync(Guid id)
    {
        var review = await _unitOfWork.ReviewRepository.GetReviewDetailAsync(id);
        if (review == null)
        {
            throw new KeyNotFoundException("Không tìm thấy đánh giá.");
        }
        return _mapper.Map<ReviewVM>(review);
    }

    public async Task<ReviewVM> AddReviewAsync(ReviewModel model)
    {
        // kiem tra proposal co ton tai khong
        var proposal = await _unitOfWork.ProposalRepository.GetProposalDetailAsync(model.ProposalId)
            ?? throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");
        if (proposal.Review != null)
        {
            throw new BadHttpRequestException("Không thể đánh giá, thỏa thuận đã được đánh giá.");
        }

        if (proposal.ProposalStatus != ProposalStateEnum.CompletePayment &&
            proposal.ProposalStatus != ProposalStateEnum.ConfirmPayment)
        {
            throw new BadHttpRequestException($"Không thể đánh giá, thỏa thuận chưa hoàn thành " +
                $"(trạng thái hiện tại là '{PROPOSALSTATE_ENUM_VN[proposal.ProposalStatus]}')");
        }

        Guid accountId = _claimService.GetCurrentUserId ?? default;
        if (proposal.OrdererId != accountId)
        {
            throw new UnauthorizedAccessException(
                "Không thể đánh giá, bạn không phải là khách hàng của thỏa thuận.");
        }

        Review newReview = _mapper.Map<Review>(model);
        await _unitOfWork.ReviewRepository.AddAsync(newReview);
        await _unitOfWork.SaveChangesAsync();
        var result = await _unitOfWork.ReviewRepository.GetByIdAsync(newReview.Id);
        return _mapper.Map<ReviewVM>(result);
    }

    public async Task<ReviewVM> UpdateReviewAsync(Guid reviewId, ReviewEM model)
    {
        Guid accountId = _claimService.GetCurrentUserId ?? default;
        var oldReview = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
        if (oldReview == null)
        {
            throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");
        }
        if (oldReview.CreatedBy != accountId)
        {
            throw new UnauthorizedAccessException("Bạn không phải người viết đánh giá này.");
        }

        oldReview.Vote = model.Vote;
        oldReview.Detail = model.Detail;
        _unitOfWork.ReviewRepository.Update(oldReview);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReviewVM>(oldReview);
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
        if (review == null)
        {
            throw new KeyNotFoundException("Không tìm thấy đánh giá.");
        }

        var currentRole = _claimService.GetCurrentRole;
        if (currentRole.Equals(RoleEnum.Moderator.ToString())
                || (currentRole.Equals(RoleEnum.CommonUser.ToString())
                && _claimService.GetCurrentUserId != review.CreatedBy))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền xóa đánh giá này.");
        }
        _unitOfWork.ReviewRepository.Delete(review);
        await _unitOfWork.SaveChangesAsync();
    }

}
