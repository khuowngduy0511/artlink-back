using Application.Services.Abstractions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly string _cloudName;
    private readonly string _apiKey;
    private readonly string _apiSecret;

    public CloudinaryService(IConfiguration configuration)
    {
        _cloudName = configuration["Cloudinary:CloudName"] ?? throw new ArgumentNullException("Cloudinary:CloudName is missing");
        _apiKey = configuration["Cloudinary:ApiKey"] ?? throw new ArgumentNullException("Cloudinary:ApiKey is missing");
        _apiSecret = configuration["Cloudinary:ApiSecret"] ?? throw new ArgumentNullException("Cloudinary:ApiSecret is missing");

        var account = new Account(_cloudName, _apiKey, _apiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string?> UploadFileAsync(IFormFile file, string fileName, string folderName, bool isPublic = true)
    {
        try
        {
            // Xác định loại file để dùng uploadParams phù hợp
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var isImage = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" }.Contains(fileExtension);

            if (isImage)
            {
                // Upload image
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = $"{folderName}/{fileName}",
                    Overwrite = true,
                    Type = isPublic ? "upload" : "authenticated"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
            }
            else
            {
                // Upload raw file (video, pdf, zip, psd, etc.)
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = $"{folderName}/{fileName}",
                    Overwrite = true,
                    Type = isPublic ? "upload" : "authenticated"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cloudinary upload error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteFileAsync(string fileName, string folderName)
    {
        try
        {
            var publicId = $"{folderName}/{fileName}";
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cloudinary delete error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Tạo signed URL cho authenticated/private file với thời gian hết hạn
    /// Dùng cho Asset download - bảo mật cao cho production
    /// </summary>
    public string GetSignedUrl(string publicId, int expirationMinutes = 60)
    {
        try
        {
            // Tạo timestamp hết hạn (Unix timestamp)
            var expirationTime = DateTimeOffset.UtcNow.AddMinutes(expirationMinutes).ToUnixTimeSeconds();
            
            // Tạo signature để xác thực theo format của Cloudinary
            // Format: timestamp={timestamp}&type=authenticated{apiSecret}
            var stringToSign = $"timestamp={expirationTime}&type=authenticated{_apiSecret}";
            var signature = ComputeSha1Hash(stringToSign);
            
            // Tạo signed URL với authenticated type
            var signedUrl = $"https://res.cloudinary.com/{_cloudName}/raw/authenticated/{publicId}?timestamp={expirationTime}&signature={signature}";
            
            Console.WriteLine($"[Cloudinary] Generated signed URL for: {publicId}, expires in {expirationMinutes} minutes");
            return signedUrl;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cloudinary signed URL error: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Helper method để tạo SHA1 hash cho signature
    /// </summary>
    private string ComputeSha1Hash(string input)
    {
        using var sha1 = System.Security.Cryptography.SHA1.Create();
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hashBytes = sha1.ComputeHash(inputBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Download file từ Cloudinary URL về dưới dạng Stream
    /// Dùng cho backend proxy download - bảo mật cao nhất
    /// </summary>
    public async Task<Stream> DownloadFileAsync(string fileUrl)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(fileUrl);
            response.EnsureSuccessStatusCode();
            
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            
            return memoryStream;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cloudinary download error: {ex.Message}");
            throw new Exception($"Không thể tải file từ Cloudinary: {ex.Message}");
        }
    }
}
