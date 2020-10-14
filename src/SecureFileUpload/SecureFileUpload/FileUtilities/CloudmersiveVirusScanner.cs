using System;
using System.Collections.Generic;
using System.IO;
using Cloudmersive.APIClient.NET.VirusScan.Api;
using Cloudmersive.APIClient.NET.VirusScan.Client;

namespace SecureFileUpload.FileUtilities
{
    public class CloudmersiveVirusScanner : IVirusScanner
    {
        private const string ApiKeySetting = "CloudmersiveVirusScanApiKey";
        private void EnsureDefaultKeyIsSet()
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings[ApiKeySetting];

            if (string.IsNullOrEmpty(apiKey))
                throw new System.Configuration.ConfigurationErrorsException($"Missing value for {ApiKeySetting}");

            Configuration.Default.AddApiKey("Apikey", apiKey);
        }
        public ScanResult ScanStream(Stream stream)
        {
            EnsureDefaultKeyIsSet();

            var apiInstance = new ScanApi();
            var result = new ScanResult();

            try
            {
                var r = apiInstance.ScanFileAdvanced(stream, false);

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