
namespace ProductManagement.Application.Interfaces
{
    public interface IFileStore
    {
        Task<string> SaveFileAsync(string fileName, byte[] fileStream);
        Task<byte[]> GetFileAsync(string fileName);
    }
}
