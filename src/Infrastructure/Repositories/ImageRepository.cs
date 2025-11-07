using Application.Services.Abstractions;
using CoenM.ImageHash;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ImageRepository : GenericRepository<Image>, IImageRepository
{
    private readonly IClaimService _claimService;

    public ImageRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext)
    {
        _claimService = claimService;
    }

    public async Task<List<Image>> GetImagesDuplicateAsync(Guid imageId)
    {
        var image = _dbContext.Images.Where(x => x.Id == imageId).FirstOrDefault()!;
        var artworkOfImage = _dbContext.Artworks.Where(x => x.Id == image.ArtworkId).FirstOrDefault();
        var createdByOfArtwork = artworkOfImage!.CreatedBy;
        //x.Artwork.State == Domain.Enums.StateEnum.Accepted
        var result = await _dbContext.Images.Where(x => x.Artwork.CreatedBy != createdByOfArtwork
        ).ToListAsync();
        List<Image> result2 = new();
        foreach (Image item in result)
        {
            var similarity = CompareHash.Similarity(item.ImageHash.GetValueOrDefault(), image.ImageHash.GetValueOrDefault());
            if (similarity > 95)
            {
                item.Similarity = similarity;
                result2.Add(item);
            }
        }
        return result2;
    }
}
