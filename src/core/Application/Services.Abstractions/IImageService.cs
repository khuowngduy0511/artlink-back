using Application.Models;
using Domain.Entitites;

namespace Application.Services.Abstractions;
public interface IImageService
{
    Task<Image?> GetImageByIdAsync(Guid imageId);
    Task<List<Image>> GetAllImagesAsync();
    Task<List<Image>> GetAllImagesOfArtworkAsync(Guid artworkId);
    Task AddImageAsync(ImageModel image);
    Task AddRangeImageAsync(MultiImageModel multiImageModel, bool IsSaveChanges = true);
    Task PutRangeImageAsync(MultiImageModel multiImageModel);
    Task UpdateImageAsync(Image image);
    Task DeleteImageAsync(Guid imageId);
    Task<List<ImageDuplicationVM>> GetImagesDuplicateAsync(Guid imageId);
}
