using Application.AppConfigurations;
using Application.Commons;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Firebase;
public class FirebaseService : IFirebaseService
{
    private readonly FirebaseConfiguration _firebaseConfiguration;

    public FirebaseService(AppConfiguration config)
    {
        _firebaseConfiguration = config.FirebaseConfiguration;
    }

    public async Task<string?> UploadFileToFirebaseStorage(IFormFile files, string fileName, string folderName)
    {
        if (files.Length > 0)
        {
            try
            {
                var task = new FirebaseStorage(
                    _firebaseConfiguration.Bucket
                    )
                    .Child(folderName)
                    .Child($"{fileName}.{Path.GetExtension(files.FileName).Substring(1)}")
                    .PutAsync(files.OpenReadStream());

                string? urlFile = await task;
                return urlFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return null;
    }

    public async Task DeleteFileInFirebaseStorage(string fileName, string folderName)
    {
        try
        {
            var task = new FirebaseStorage(
                _firebaseConfiguration.Bucket,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child(folderName)
                .Child(fileName)
                .DeleteAsync();

            await task;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string?> DownloadFileFromFirebaseStorage(string fileName, string folderName)
    {
        try
        {
            var task = new FirebaseStorage(
                _firebaseConfiguration.Bucket,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child(folderName)
                .Child(fileName).
                GetDownloadUrlAsync();

            var downloadUrl = await task;
            return downloadUrl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<FirebaseMetaData?> GetMetadataFileFromFirebaseStorage(string fileName, string folderName)
    {
        try
        {
            var task = new FirebaseStorage(
                _firebaseConfiguration.Bucket,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child(folderName)
                .Child(fileName).
                GetMetaDataAsync();

            var metadata = await task;
            return metadata;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
