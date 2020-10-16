namespace SecureFileUploadService
{
    public class ScanResult
    {
        public ScanResult()
        {
            this.Message = string.Empty;
        }

        public bool IsSafe { get; set; }
        public string Message { get; set; }
    }
}