namespace SecureFileUploadService
{
    public class CsvItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CsvItemType Type { get; set; }
    }

    public enum CsvItemType
    {
        None,
        New,
        Update
    }
}