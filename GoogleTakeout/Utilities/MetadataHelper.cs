using MetadataExtractor.Formats.Exif;
using MetadataExtractor;
using System.Globalization;
using GoogleTakeout.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace GoogleTakeout.Utilities
{
    public static class MetadataHelper
    {
        public static DateTime? GetCreatedDate(string filePath)
        {
            var directories = ImageMetadataReader.ReadMetadata(filePath);
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
            if (filePath.EndsWith(".mp4"))
            {
                //Fri Nov 10 03:06:21 2023
                var tags = directories.FirstOrDefault(x => x.Name == "QuickTime Movie Header")?.Tags;
                if (tags != null && tags.Count > 0)
                {
                    dateTime = tags
                        .Where(x => x != null)
                        .FirstOrDefault(x => x.Name == "Created")?
                        .Description;

                    if (DateTime.TryParseExact(dateTime, "ddd MMM dd HH:mm:ss yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime movieDate))
                        return movieDate;
                }

            }

            if (DateTime.TryParseExact(dateTime, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                return date;

            return null;
        }

        public static Dictionary<string, Metadata> GetMetaData(IEnumerable<string> jsonFiles)
        {
            var result = new Dictionary<string, Metadata>();
            foreach (var filePath in jsonFiles)
            {
                var content = File.ReadAllText(filePath);
                if (content == null) continue;

                try
                {
                    var metadata = JsonConvert.DeserializeObject<Metadata>(
                                    content,
                                    new JsonSerializerSettings()
                                    {
                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                    });
                    if (!string.IsNullOrEmpty(metadata?.Title))
                    {
                        result.Add(filePath.Replace(".json", "", StringComparison.OrdinalIgnoreCase), metadata);
                    }

                }
                catch (Exception ex)
                {
                }

            }

            return result;
        }
    }
}
