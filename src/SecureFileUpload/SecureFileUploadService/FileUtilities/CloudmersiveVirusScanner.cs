using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cloudmersive.APIClient.NETCore.VirusScan.Api;
using Cloudmersive.APIClient.NETCore.VirusScan.Client;
using Microsoft.Extensions.Configuration;

namespace SecureFileUploadService
{
    public class CloudmersiveVirusScanner : IVirusScanner
    {
        private const string ApiKeySetting = "CloudmersiveVirusScanApiKey";

        public void SetConfiguration(IConfiguration configuration)
        {
            string apiKey = configuration[ApiKeySetting];

            if (string.IsNullOrEmpty(apiKey))
                throw new ApplicationException($"Missing value for {ApiKeySetting}");

            Configuration.Default.AddApiKey("Apikey", apiKey);
        }

        public async Task<ScanResult> ScanStreamAsync(Stream stream)
        {
            var apiInstance = new ScanApi();
            var result = new ScanResult();

            try
            {
                // ensure stream is ready for read
                stream.Position = 0;
                var r = await apiInstance.ScanFileAdvancedAsync(stream, false);

                if (r.CleanResult.HasValue)
                {
                    result.IsSafe = true;
                }
                else if (r.FoundViruses != null && r.FoundViruses.Count > 0)
                {
                    var viruses = new List<string>();
                    r.FoundViruses.ForEach(v => viruses.Add(v.VirusName));
                    result.Message = $"Virus found: {string.Join(",", viruses)}";
                }
            }
            catch (Exception e)
            {
                result.Message = $"Unable to complete scan, {e.Message}";
            }

            return result;
        }
    }
}