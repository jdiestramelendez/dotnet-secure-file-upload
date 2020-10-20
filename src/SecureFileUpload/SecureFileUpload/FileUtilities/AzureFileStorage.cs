using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SecureFileUpload.FileUtilities
{
    public class AzureFileStorage : IFileStorage
    {
        private const string ContainerName = "secure-file-upload";
        private BlobServiceClient blobServiceClient;
        private BlobContainerClient containerClient;

        public AzureFileStorage()
        {
            string connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];
            blobServiceClient = new BlobServiceClient(connectionString);
            containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            if (containerClient == null || !containerClient.Exists())
                containerClient = blobServiceClient.CreateBlobContainer(ContainerName);
        }

        public void DeleteFile(string name)
        {
            containerClient.DeleteBlobIfExists(name);
        }

        public List<string> GetFiles()
        {
            var files = new List<string>();

            foreach (BlobItem blobItem in containerClient.GetBlobs())
            {
                files.Add(blobItem.Name);
            }

            return files;
        }

        public void SavePostedFile(string name, Stream stream)
        {
            var blobname = System.IO.Path.GetFileName(name);
            containerClient.UploadBlob(blobname, stream);
        }
    }
}