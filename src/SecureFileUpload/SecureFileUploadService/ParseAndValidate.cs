using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SecureFileUploadService
{
    public static class ParseAndValidate
    {
        [FunctionName("ParseAndValidate")]
        [return: ServiceBus("valid")]
        public static async Task<string> Run(
            [ServiceBusTrigger("parse-and-validate")] string myQueueItem,
            [Blob("%AzureStorage:Container%", FileAccess.Read, Connection = "AzureStorage:ConnectionString")] CloudBlobContainer cloudBlobContainer,
            ILogger log)
        {
            log.LogInformation($"Parsing and validating: {myQueueItem}");

            // Download Blob Content
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(myQueueItem);
            var memoryStream = new MemoryStream();
            await cloudBlockBlob.DownloadToStreamAsync(memoryStream);

            // Parse and Validate File
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            FileTrackerRepository.AddOperationResult(myQueueItem, "Parse and Validate");

            var parseErrors = CsvFile.Validate(memoryStream);
            stopWatch.Stop();

            FileTrackerRepository.UpdateOperationResult(myQueueItem, "Parse and Validate", stopWatch.ElapsedMilliseconds, true);

            if (parseErrors.Count > 0)
            {
                log.LogInformation($"Parsing and validating results for: {myQueueItem}, {string.Join(",", parseErrors)}");
                throw new ApplicationException("File is not valid");
            }
            else
                log.LogInformation($"Parsing and validating results for: {myQueueItem}, looks good for next stage");

            return myQueueItem;
        }
    }
}
