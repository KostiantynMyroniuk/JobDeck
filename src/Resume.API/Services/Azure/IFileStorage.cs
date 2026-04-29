namespace Resume.API.Services.Azure
{
    public interface IFileStorage
    {
        Task<(string fileUrl, string fileName)> UploadFileAsync(IFormFile file, string containerName, CancellationToken ct = default);
        string GetFileUrl(string fileName, string containerName, CancellationToken ct = default);
        Task DeleteFileAsync(string fileName, string containerName, CancellationToken ct = default);
    }
}
