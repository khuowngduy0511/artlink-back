using Microsoft.AspNetCore.Http;

namespace Application.Services.Abstractions;

public interface ICloudinaryService
{
    Task<string?> UploadFileAsync(IFormFile file, string fileName, string folderName, bool isPublic = true);
    Task<bool> DeleteFileAsync(string fileName, string folderName);
    
    /// <summary>
    /// Tạo signed URL có thời hạn cho private/authenticated files
    /// </summary>
    string GetSignedUrl(string publicId, int expirationMinutes = 60);
    
    /// <summary>
    /// Download file từ Cloudinary URL về Stream (dùng cho backend proxy)
    /// </summary>
    Task<Stream> DownloadFileAsync(string fileUrl);
}
