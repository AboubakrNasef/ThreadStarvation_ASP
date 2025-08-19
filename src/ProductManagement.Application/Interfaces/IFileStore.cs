using System.IO;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.FileStore
{
    public interface IFileStore
    {
        Task<string> SaveFileAsync(string fileName, byte[] fileStream);
        Task<Stream?> GetFileAsync(string fileName);
    }
}
