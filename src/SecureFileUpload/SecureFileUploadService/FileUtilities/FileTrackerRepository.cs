using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace SecureFileUploadService
{
    static class FileTrackerRepository
    {
        private static ConcurrentDictionary<string, FileTracker> fileTrackers = new ConcurrentDictionary<string, FileTracker>();

        public static FileTracker AddNew(string originalFileName, string fileName, long elapsedTimeInMilliseconds)
        {
            var fileTracker = new FileTracker { OriginalFileName = originalFileName, FileName = fileName };
            fileTracker.Operations.Add(new OperationResult { Name = "Storage", ElapsedTimeInMilliseconds = elapsedTimeInMilliseconds });
            fileTrackers.TryAdd(fileName, fileTracker);
            return fileTracker;
        }

        public static FileTracker Get(string fileName)
        {
            FileTracker fileTracker = null;
            fileTrackers.TryGetValue(fileName, out fileTracker);
            return fileTracker;
        }

        public static FileTracker AddNewOperationResult(string fileName, string name, long elapsedTimeInMilliseconds)
        {
            var fileTracker = Get(fileName);

            fileTracker.Operations.Add(new OperationResult { Name = name, ElapsedTimeInMilliseconds = elapsedTimeInMilliseconds });

            return fileTracker;
        }
    }
}
