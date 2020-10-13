using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace SecureFileUpload.Models
{
    public class CsvItemMapping : CsvMapping<CsvItem>
    {
        public CsvItemMapping()
        {
            MapProperty(0, x => x.ID);
            MapProperty(1, x => x.Name);
            MapProperty(2, x => x.Description);
            MapProperty(3, x => x.Type, new EnumConverter<CsvItemType>());
        }
    }
}