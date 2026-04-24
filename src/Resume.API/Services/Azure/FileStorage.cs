using Azure.Storage.Blobs;

namespace Resume.API.Services.Azure
{
    public class FileStorage : IFileStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<(string fileUrl, string fileName)> UploadFileAsync(IFormFile file, string containerName, CancellationToken ct = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync(cancellationToken: ct);

            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            var blob = container.GetBlobClient(fileName);

            await using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream, cancellationToken: ct);

            return (blob.Uri.ToString(), fileName);
        }

        public async Task<Stream> DownloadFileAsync(string fileName, string containerName, CancellationToken ct = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(fileName);
            var download = await blob.DownloadAsync(cancellationToken: ct);

            return download.Value.Content;
        }

        public async Task DeleteFileAsync(string fileName, string containerName, CancellationToken ct = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(fileName);

            await blob.DeleteIfExistsAsync(cancellationToken: ct);
        }
    }
}
