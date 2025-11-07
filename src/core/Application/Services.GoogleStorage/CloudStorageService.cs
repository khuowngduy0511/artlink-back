using Application.AppConfigurations;
using Application.Commons;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Application.Services.GoogleStorage;

public class CloudStorageService : ICloudStorageService
{
    private static readonly int EXPIRATION_TIME_HOUR = 1;
    private readonly GoogleStorageConfiguration _googleStorageConfiguration;

    public CloudStorageService(AppConfiguration config)
    {
        _googleStorageConfiguration = config.GoogleStorageConfiguration;
    }

    public async Task<string?> UploadFileToCloudStorage(IFormFile files, string fileName, string folderName, bool? isPublic = true)
    {
        try
        {
            // Validate input
            if (files == null)
            {
                throw new ArgumentNullException(nameof(files), "File không được để trống");
            }
            
            if (string.IsNullOrEmpty(files.FileName))
            {
                throw new ArgumentException("Tên file không hợp lệ", nameof(files));
            }
            
            var client = StorageClient.Create();
            
            // Get file extension safely
            var fileExtension = Path.GetExtension(files.FileName);
            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new ArgumentException("File phải có định dạng (extension)", nameof(files));
            }
            
            // Remove the dot from extension
            fileExtension = fileExtension.TrimStart('.');
            
            string folderAndFileName = $"{folderName}/{fileName}.{fileExtension}";
            if (isPublic == true)
            {
                var obj = await client.UploadObjectAsync(
                    _googleStorageConfiguration.BucketPublic,
                    folderAndFileName,
                    files.ContentType,
                    files.OpenReadStream());
                string publicUrl = $"https://storage.googleapis.com/{_googleStorageConfiguration.BucketPublic}/{folderAndFileName}";

                return publicUrl;
            }
            else
            {
                var obj = await client.UploadObjectAsync(
                    _googleStorageConfiguration.BucketPrivate,
                    folderAndFileName,
                    files.ContentType,
                    files.OpenReadStream());

                return obj.MediaLink;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteFileInCloudStorage(string fileName, string folderName, bool? isPublic = true)
    {
        try
        {
            var client = StorageClient.Create();
            if (isPublic == true)
            {
                await client.DeleteObjectAsync(_googleStorageConfiguration.BucketPublic, $"{folderName}/{fileName}");
            }
            else
            {
                await client.DeleteObjectAsync(_googleStorageConfiguration.BucketPrivate, $"{folderName}/{fileName}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<MemoryStream> DownloadFileFromPrivateCloudStorage(string fileName, string folderName)
    {
        try
        {
            var client = StorageClient.Create();
            var stream = new MemoryStream();
            var obj = await client.DownloadObjectAsync(_googleStorageConfiguration.BucketPrivate, $"{folderName}/{fileName}", stream);
            stream.Position = 0;

            return stream;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string?> GetDownloadSignedUrlFromPrivateCloudStorage(string fileName, string folderName)
    {
        try
        {
            var client = await StorageClient.CreateAsync();
            UrlSigner urlSigner = await UrlSigner.FromCredentialFileAsync("Credentials/account_service.json");

            string url = await urlSigner.SignAsync(_googleStorageConfiguration.BucketPrivate,
                $"{folderName}/{fileName}", TimeSpan.FromHours(EXPIRATION_TIME_HOUR), HttpMethod.Get);

            return url;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
