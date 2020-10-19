using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;

namespace SecureFileUpload.FileUtilities
{
    public class LocalFileStorage : IFileStorage
    {
        private string basePath;

        public LocalFileStorage()
        {
#if DEBUG
            this.basePath = HttpContext.Current.Server.MapPath("App_Data");
#else
            this.basePath = Environment.GetEnvironmentVariable("TMP");
#endif
        }

        private string ResolveFullPath(string filename)
        {
            return Path.Combine(this.basePath, filename);
        }

        public void DeleteFile(string filename)
        {
            string fullpath = ResolveFullPath(filename);
            if (File.Exists(fullpath))
                File.Delete(fullpath);
        }

        public List<string> GetFiles()
        {
            var results = new List<string>();

            foreach (string file in Directory.GetFiles(this.basePath))
            {
                results.Add(Path.GetFileName(file));
            }

            return results;
        }

        public void SavePostedFile(string name, Stream stream)
        {
            string path = ResolveFullPath(Path.GetFileName(name));
            using (var outputFileStream = new FileStream(path, FileMode.Create))
                stream.CopyTo(outputFileStream);
        }
    }
}