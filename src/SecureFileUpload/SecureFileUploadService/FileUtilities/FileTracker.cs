using System.Collections.Generic;

namespace SecureFileUploadService
{
    class FileTracker
    {
        public FileTracker()
        {
            Operations = new List<OperationResult>();
        }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public bool ProcessingComplete { get; set; }
        public List<OperationResult> Operations { get; set; }
    }
}
