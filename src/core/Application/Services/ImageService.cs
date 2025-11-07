using Application.Models;
using Application.Services.Abstractions;
using Application.Services.GoogleStorage;
using AutoMapper;
using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Services;
public class ImageService : IImageService
{
    private static readonly string PARENT_FOLDER = "Image";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudStorageService _cloudStorageService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;

    public ImageService(
        IUnitOfWork unitOfWork,
        ICloudStorageService cloudStorageService,
        ICloudinaryService cloudinaryService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _cloudStorageService = cloudStorageService;
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
    }

    public async Task<List<Image>> GetAllImagesAsync()
    {
        return await _unitOfWork.ImageRepository.GetAllAsync();
    }

    public async Task<Image?> GetImageByIdAsync(Guid imageId)
    {
        var result = await _unitOfWork.ImageRepository.GetByIdAsync(imageId);
        return result;
    }

    public Task<List<Image>> GetAllImagesOfArtworkAsync(Guid artworkId)
    {
        var result = _unitOfWork.ImageRepository.GetListByConditionAsync(x => x.ArtworkId == artworkId);
        return result;
    }

    // lay so thu tu lon nhat cua hinh anh trong artwork
    private async Task<int> GetLatestOrderOfImageInArtwork(Guid artworkId)
    {
        var allImagesOfArtwork = await _unitOfWork.ImageRepository.GetListByConditionAsync(x => x.ArtworkId == artworkId);
        return allImagesOfArtwork.Count;
    }

    public async Task AddImageAsync(ImageModel imageModel)
    {
        // kiem tra artwork co ton tai khong
        bool IsArtworkExisted = await _unitOfWork.ArtworkRepository.IsExistedAsync(imageModel.ArtworkId);
        if (!IsArtworkExisted)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");

        // lay stt cua hinh anh, dat ten lai hinh anh
        int latestOrder = await GetLatestOrderOfImageInArtwork(imageModel.ArtworkId);
        string newImageName = imageModel.ArtworkId + "_i" + latestOrder;
        string folderName = PARENT_FOLDER;
        string imageExtension = Path.GetExtension(imageModel.Image.FileName); // lay duoi file (.png, .jpg, ...)

        //upload hinh anh len cloud, lay url
        var url = await _cloudStorageService.UploadFileToCloudStorage(imageModel.Image, newImageName, folderName)
            ?? throw new KeyNotFoundException("Lỗi khi tải hình ảnh lên đám mây.");

        // hashing image de kiem tra trung anh
        var hashAlgorithm = new PerceptualHash();
        ulong imageHash = hashAlgorithm.Hash(imageModel.Image.OpenReadStream());

        // luu thong tin hinh anh vao database
        Image image = _mapper.Map<Image>(imageModel);
        image.Location = url;
        image.ImageName = newImageName + imageExtension;
        image.Order = latestOrder;
        image.ImageHash = imageHash;
        await _unitOfWork.ImageRepository.AddAsync(image);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ImageDuplicationVM>> GetImagesDuplicateAsync(Guid imageId)
    {
        var isExisted = await _unitOfWork.ImageRepository.IsExistedAsync(imageId);
        if (!isExisted)
            throw new KeyNotFoundException("Không tìm thấy hình ảnh.");

        var result = await _unitOfWork.ImageRepository.GetImagesDuplicateAsync(imageId);
        return _mapper.Map<List<ImageDuplicationVM>>(result);
    }

    public async Task AddRangeImageAsync(MultiImageModel multiImageModel, bool isSaveChanges = true)
    {
        #region validate
        // kiem tra artwork co ton tai khong
        bool IsArtworkExisted = await _unitOfWork.ArtworkRepository.IsExistedAsync(multiImageModel.ArtworkId);
        if (!IsArtworkExisted)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");

        // chi cho phep upload nhieu hinh anh cho artwork moi tao
        var listImage = await _unitOfWork.ImageRepository.GetListByConditionAsync(x => x.ArtworkId == multiImageModel.ArtworkId);
        if (listImage.Count > 0)
            throw new BadHttpRequestException("Tác phẩm đã có bộ hình ảnh, không thể thêm ảnh lần nữa (chỉ cho phép thêm hình ảnh trong lần đầu tiên)");
        #endregion

        #region upload range image (optimize)
        var uploadImagesTask = new List<Task>();
        foreach (var singleImage in multiImageModel.Images.Select((image, index) => (image, index)))
        {
            Guid artworkId = multiImageModel.ArtworkId;

            string newImageName = artworkId + "_i" + singleImage.index;
            string folderName = PARENT_FOLDER;
            string imageExtension = Path.GetExtension(singleImage.image.FileName); // lay duoi file (.png, .jpg, ...)

            // Upload hình ảnh lên Cloudinary
            uploadImagesTask.Add(Task.Run(async () =>
            {
                var url = await _cloudinaryService.UploadFileAsync(singleImage.image, newImageName, folderName)
                    ?? throw new KeyNotFoundException("Lỗi khi tải bộ hình ảnh lên Cloudinary.");

                // hashing image de kiem tra trung anh
                var hashAlgorithm = new PerceptualHash();
                ulong imageHash = hashAlgorithm.Hash(singleImage.image.OpenReadStream());

                // luu thong tin hinh anh vao database
                Image image = new()
                {
                    ArtworkId = artworkId,
                    Location = url,
                    ImageName = newImageName + imageExtension,
                    Order = singleImage.index,
                    ImageHash = imageHash
                };
                await _unitOfWork.ImageRepository.AddAsync(image);
            }));
        }
        await Task.WhenAll(uploadImagesTask);
        #endregion

        if (isSaveChanges)
            await _unitOfWork.SaveChangesAsync();
    }

    public async Task PutRangeImageAsync(MultiImageModel multiImageModel)
    {
        var listOldImage = await _unitOfWork.ImageRepository.GetListByConditionAsync(x => x.ArtworkId == multiImageModel.ArtworkId);
        if (listOldImage != null)
        {
            foreach (var image in listOldImage)
            {
                await _cloudStorageService.DeleteFileInCloudStorage(image.ImageName, $"{PARENT_FOLDER}/Image");
                _unitOfWork.ImageRepository.Delete(image);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        foreach (var singleImage in multiImageModel.Images.Select((image, index) => (image, index)))
        {
            string newImageName = multiImageModel.ArtworkId + "_i" + singleImage.index;
            string folderName = PARENT_FOLDER;
            string imageExtension = Path.GetExtension(singleImage.image.FileName); // lay duoi file (.png, .jpg, ...)

            //upload hinh anh len cloud, lay url
            var url = await _cloudStorageService.UploadFileToCloudStorage(singleImage.image, newImageName, folderName)
                ?? throw new KeyNotFoundException("Lỗi khi tải bộ hình ảnh lên đám mây.");

            // luu thong tin hinh anh vao database
            Image image = new()
            {
                ArtworkId = multiImageModel.ArtworkId,
                Location = url,
                ImageName = newImageName + imageExtension,
                Order = singleImage.index
            };
            await _unitOfWork.ImageRepository.AddAsync(image);
        }

    }

    public async Task DeleteImageAsync(Guid imageId)
    {
        var result = await _unitOfWork.ImageRepository.GetByIdAsync(imageId);
        if (result == null)
            throw new KeyNotFoundException("Không tìm thấy hình ảnh.");

        await _cloudStorageService.DeleteFileInCloudStorage(result.ImageName, PARENT_FOLDER);
        _unitOfWork.ImageRepository.Delete(result);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateImageAsync(Image image)
    {
        _unitOfWork.ImageRepository.Update(image);
        await _unitOfWork.SaveChangesAsync();
    }
}
