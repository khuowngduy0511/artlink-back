namespace Application.Services.Abstractions;

public interface IClaimService
{
    public Guid? GetCurrentUserId { get; }
    public string GetCurrentUserName { get; }
    public string GetCurrentRole { get; }
    public bool IsAuthorized(Guid accountId);
    public bool IsModeratorOrAdmin();
}
