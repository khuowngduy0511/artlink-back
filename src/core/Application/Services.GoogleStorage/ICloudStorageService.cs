using Microsoft.AspNetCore.Http;

namespace Application.Services.GoogleStorage;

public interface ICloudStorageService
{

    /// <summary>
    /// Upload file len cloud storage, cung co the thay the file cung duong dan va ten
    /// </summary>
    /// <returns>tra ve url file</returns>
    Task<string?> UploadFileToCloudStorage(IFormFile files, string fileName, string folderName, bool? isPublic = true);

    /// <summary>
    /// Xoa file tren storage
    /// </summary>
    Task DeleteFileInCloudStorage(string fileName, string folderName, bool? isPublic = true);

    /// <summary>
    /// lay url download file tu storage
    /// </summary>
    /// <returns>lay url download file tu storage</returns>
    Task<MemoryStream> DownloadFileFromPrivateCloudStorage(string fileName, string folderName);

    Task<string?> GetDownloadSignedUrlFromPrivateCloudStorage(string fileName, string folderName);

}
