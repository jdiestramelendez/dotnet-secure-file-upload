using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SecureFileUploadService
{
    public static class GetFileTracker
    {
        [FunctionName("GetFileTracker")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "FileTracker/{filename}")] HttpRequest req,
            string filename,
            ILogger log)
        {
            log.LogInformation($"Retrieving File Tracker for {filename}");

            return new OkObjectResult(FileTrackerRepository.Get(filename));
        }
    }
}
