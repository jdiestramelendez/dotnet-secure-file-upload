using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureFileUpload.Models
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