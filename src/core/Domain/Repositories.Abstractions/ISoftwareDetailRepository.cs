using Domain.Entitites;

namespace Domain.Repositories.Abstractions;

public interface ISoftwareDetailRepository
{
    Task<List<SoftwareDetail>> GetAllSoftwareDetailsOfSoftwareAsync(Guid artworkId);
    Task AddSoftwareDetailAsync(SoftwareDetail softwareDetail);
    void DeleteSoftwareDetail(SoftwareDetail softwareDetail);
}
