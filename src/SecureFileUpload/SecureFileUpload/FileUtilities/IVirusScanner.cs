using System.IO;

namespace SecureFileUpload.FileUtilities
{
    public interface IVirusScanner
    {
        ScanResult ScanStream(Stream stream);
    }
}