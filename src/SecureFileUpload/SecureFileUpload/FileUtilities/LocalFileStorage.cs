using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SecureFileUpload.FileUtilities
{
    public class LocalFileStorage : IFileStorage
    {
        private string basePath;

        public LocalFileStorage(string basePath)
        {
            this.basePath = basePath;
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

        public void SavePostedFile(HttpPostedFile postedFile)
        {
            string path = ResolveFullPath(Path.GetFileName(postedFile.FileName));
            postedFile.SaveAs(path);
        }
    }
}