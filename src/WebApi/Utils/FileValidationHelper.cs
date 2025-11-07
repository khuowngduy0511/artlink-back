namespace WebApi.Utils;

public static class FileValidationHelper
{
    private static readonly string[] ALLOW_IMAGE_EXTENSIONS = {
        ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"
    };

    private static readonly string[] ALLOW_ASSET_EXTENSIONS = new[] {
        ".abr", ".ai", ".ase", ".dng", ".doc", ".docx", ".eps", ".gif", ".indd", ".jpeg",
        ".jpg", ".otf", ".pdf", ".png", ".ppt", ".pptx", ".psd", ".raw", ".svg", ".tif", ".tiff",
        ".ttf", ".txt", ".webp", ".woff", ".woff2", ".xls", ".xlsx", ".xmp", ".zip", ".rar"
    };
    private static readonly long MAX_AVATAR_FILE_SIZE = 5 * 1024 * 1024; // 5 MB
    private static readonly long MAX_IMAGE_FILE_SIZE = 16 * 1024 * 1024; // 16 MB
    private static readonly long MAX_ASSET_FILE_SIZE = 500 * 1024 * 1024; // 500 MB

    public static bool IsImageFormatValid(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName).ToLower();
        return ALLOW_IMAGE_EXTENSIONS.Contains(fileExtension);
    }
    public static bool IsAssetFormatValid(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName).ToLower();
        return ALLOW_ASSET_EXTENSIONS.Contains(fileExtension);
    }

    public static bool IsImageFileSizeValid(IFormFile imageFile)
    {
        if (imageFile.Length > MAX_IMAGE_FILE_SIZE)
        {
            return false;
        }
        return true;
    }

    public static bool IsAvatarFileSizeValid(IFormFile imageFile)
    {
        if (imageFile.Length > MAX_AVATAR_FILE_SIZE)
        {
            return false;
        }
        return true;
    }

    public static bool IsAssetFileSizeValid(IFormFile imageFile)
    {
        if (imageFile.Length > MAX_ASSET_FILE_SIZE)
        {
            return false;
        }
        return true;
    }
}
