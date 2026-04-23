namespace Resume.API.Services.Azure
{
    public interface IFileStorage
    {
        Task<string> UploadFileAsync(IFormFile file, string containerName, CancellationToken ct = default);
        Task<Stream> DownloadFileAsync(string fileName, string containerName, CancellationToken ct = default);
    }
}
