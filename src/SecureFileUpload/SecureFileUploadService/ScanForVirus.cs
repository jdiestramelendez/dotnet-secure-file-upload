using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SecureFileUploadService
{
    public static class ScanForVirus
    {
        [FunctionName("ScanForVirus")]
        [return: ServiceBus("parse-and-validate")]
        public static async Task<string> Run(
            [ServiceBusTrigger("scan-for-virus")] string myQueueItem,
            [Blob("%AzureStorage:Container%", FileAccess.Read, Connection = "AzureStorage:ConnectionString")] CloudBlobContainer cloudBlobContainer,
            ILogger log)
        {
            log.LogInformation($"Scanning {myQueueItem}");

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            // Download Blob Content
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(myQueueItem);
            var memoryStream = new MemoryStream();
            await cloudBlockBlob.DownloadToStreamAsync(memoryStream);
            // Scan for Viruses
            var virusScanner = new CloudmersiveVirusScanner();
            virusScanner.SetConfiguration(config);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var scanResult = await virusScanner.ScanStreamAsync(memoryStream);
            stopWatch.Stop();

            FileTrackerRepository.AddNewOperationResult(myQueueItem, "Virus Scan", stopWatch.ElapsedMilliseconds);

            log.LogInformation($"Scan Results for {myQueueItem}, Safe: {scanResult.IsSafe}, Message: {scanResult.Message}");

            return myQueueItem;
        }
    }
}
