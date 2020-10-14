using System.IO;

namespace SecureFileUpload.FileUtilities
{
    public class StreamUtility
    {
        public static MemoryStream CopyToMemoryStream(Stream stream)
        {
            var memoryStream = new MemoryStream();
            byte[] buffer = new byte[16 * 1024];
            int read;

            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                memoryStream.Write(buffer, 0, read);
            }

            // reset to make available for read
            stream.Position = 0;
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}