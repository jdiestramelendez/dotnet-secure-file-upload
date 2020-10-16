using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using TinyCsvParser;

namespace SecureFileUploadService
{
    public class CsvFile
    {
        /// <summary>
        /// Returns a list of errors if any present.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static List<string> Validate(Stream stream)
        {
            var csvParserOptions = new CsvParserOptions(true, ',');
            var csvMapper = new CsvItemMapping();
            var csvParser = new CsvParser<CsvItem>(csvParserOptions, csvMapper);

            stream.Position = 0;
            var parsedItems = csvParser
                .ReadFromStream(stream, Encoding.ASCII)
                .ToList();

            var parseErrors = new List<string>();

            foreach (var parsedItem in parsedItems)
            {
                if (!parsedItem.IsValid)
                {
                    parseErrors.Add($"Row {parsedItem.RowIndex}, Problem: {parsedItem.Error.Value}");
                }
            }

            return parseErrors;
        }
    }
}