using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace GoogleTakeout.Utilities
{
    public static class FileHelper
    {
        public static void WriteToCsv(string destination, string filePath, string result, string reason)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var stream = File.Open(Path.Combine(destination, "errors", "errors.csv"), FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(new List<object>() { new { FilePath = filePath, Result = result, Reason = reason } });
            }
        }
    }
}
