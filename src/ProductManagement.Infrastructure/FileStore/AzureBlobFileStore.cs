using Azure.Storage.Blobs;


namespace ProductManagement.Infrastructure.FileStore
{
    public class AzureBlobFileStore
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

        public async Task<Stream?> GetFileAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            if (!await blobClient.ExistsAsync())
                return null;
            var response = await blobClient.OpenReadAsync();
            return response;
        }
    }
}
