using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Storage.Blob;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;

namespace SecureFileUploadService
{
    public static class FileUpload
    {
        [FunctionName("FileUpload")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            [Blob("%AzureStorage:Container%", FileAccess.Write, Connection = "AzureStorage:ConnectionString")] CloudBlobContainer cloudBlobContainer)
        {
            log.LogInformation("File Upload in progress!");

            var content = await req.ReadFormAsync();
            var blobNames = new List<string>();

            foreach (var file in content.Files)
            {
                var blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}".Replace("\"", "");
                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                var xff = req.Headers.FirstOrDefault(x => x.Key == "X-Forwarded-For").Value.FirstOrDefault();

                blobNames.Add(blobName);
                cloudBlockBlob.Properties.ContentDisposition = file.ContentDisposition;
                cloudBlockBlob.Properties.ContentType = file.ContentType;
                cloudBlockBlob.Metadata.Add("originalName", file.FileName);
                if (!string.IsNullOrEmpty(xff))
                    cloudBlockBlob.Metadata.Add("sourceIp", xff);

                log.LogInformation($"Uploading {file.FileName} as {blobName}");

                using (var fileStream = file.OpenReadStream())
                {
                    await cloudBlockBlob.UploadFromStreamAsync(fileStream);
                }

                // Add message in queue for next step
                var config = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();
                var serviceBusConnection = config["AzureWebJobsServiceBus"];
                var queueName = "scan-for-virus";

                var messageSender = new MessageSender(serviceBusConnection, queueName);
                var message = new Message(Encoding.UTF8.GetBytes(blobName));

                await messageSender.SendAsync(message).ConfigureAwait(false);
                log.LogInformation($"Added message in {queueName}!");
            }

            return new OkObjectResult(new { blobs = blobNames });
        }
    }
}
