using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace SecureFileUploadService
{
    public interface IVirusScanner
    {
        void SetConfiguration(IConfiguration configuration);
        Task<ScanResult> ScanStreamAsync(Stream stream);
    }
}