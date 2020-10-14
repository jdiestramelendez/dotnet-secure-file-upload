using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SecureFileUpload.FileUtilities
{
    public interface IFileStorage
    {
        List<string> GetFiles();
        void SavePostedFile(string name, Stream stream);
        void DeleteFile(string name);
    }
}