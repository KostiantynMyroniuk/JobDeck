using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

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
            var blobHeader = new BlobHttpHeaders
            {
                ContentType = "application/pdf"
            };

            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync(cancellationToken: ct);

            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            var blob = container.GetBlobClient(fileName);

            await using var stream = file.OpenReadStream();
            await blob.UploadAsync(
                stream, 
                new BlobUploadOptions { HttpHeaders = blobHeader }, 
                cancellationToken: ct
            );

            return (blob.Uri.ToString(), fileName);
        }

        public string GetFileUrl(string fileName, string containerName, CancellationToken ct = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);

            var blob = container.GetBlobClient(fileName);

            var url = blob.GenerateSasUri(
                BlobSasPermissions.Read,
                DateTimeOffset.UtcNow.AddMinutes(15)
            );

            return url.ToString();
        }

        public async Task DeleteFileAsync(string fileName, string containerName, CancellationToken ct = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(fileName);

            await blob.DeleteIfExistsAsync(cancellationToken: ct);
        }
    }
}
