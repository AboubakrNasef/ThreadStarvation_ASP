using Azure.Storage.Blobs;
using ProductManagement.Application.Interfaces;
using System.Reflection;


namespace ProductManagement.Infrastructure.FileStore
{
    public class AzureBlobFileStore : IFileStore
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobFileStore(string connectionString, string containerName)
        {
            var serviceClient = new BlobServiceClient(connectionString);
            _containerClient = serviceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<string> SaveFileAsync(string fileName, byte[] filebytes)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            using var memoryStream = new MemoryStream(filebytes);
            var result = await blobClient.UploadAsync(memoryStream, true);

            return fileName;
        }

        public async Task<byte[]> GetFileAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            if (!await blobClient.ExistsAsync())
                return Array.Empty<byte>();

            var response = await blobClient.DownloadContentAsync();
            return response.Value.Content.ToArray();
        }
    }
}
