using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SecureFileUpload.FileUtilities
{
    public interface IFileStorage
    {
        List<string> GetFiles();
        void SavePostedFile(HttpPostedFile postedFile);
        void DeleteFile(string name);
    }
}