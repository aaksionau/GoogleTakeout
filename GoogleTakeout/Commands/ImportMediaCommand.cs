using ByteSizeLib;
using GoogleTakeout.Models;
using GoogleTakeout.Utilities;

namespace GoogleTakeout.Commands
{
    public class ImportMediaCommand : ConsoleAppBase
    {
        [RootCommand]
        public void ImportMedia(
            [Option("s", "Path to the folder where you unzipped all zip files.")] string source,
            [Option("d", "Path to destination folder.")] string destination)
        {
            var allFiles = DirectoryHelper.GetAllFilesWithExtension(
                source, 
                new List<string>(), 
                new List<string>() { ".jpg", ".mp4", ".json" });

            if (Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            Directory.CreateDirectory(Path.Combine(destination, "noDate"));
            Directory.CreateDirectory(Path.Combine(destination, "errors"));

            var mediaFiles = allFiles.Where(x => !x.EndsWith(".json", StringComparison.OrdinalIgnoreCase)).ToList();
            var jsonFiles = allFiles.Except(mediaFiles).ToList().Where(x => x != null);

            var jsonMetadatas = MetadataHelper.GetMetaData(jsonFiles);

            var index = 0;
            foreach (string filePath in mediaFiles)
            {
                index++;
                var file = new FileInfo(filePath);
                Console.WriteLine($"Processing {index} out of {mediaFiles.Count}");
                Console.WriteLine($"--> Processing: {file.Name}, {ByteSize.FromBytes(file.Length).MegaBytes} Mb");

                try
                {
                    var newPath = Path.Combine(destination, file.Name);
                    var parsedDateTime = MetadataHelper.GetCreatedDate(filePath);
                    jsonMetadatas.TryGetValue(filePath, out Metadata? metadata);

                    if (!parsedDateTime.HasValue && metadata == null)
                    {
                        File.Copy(filePath, $"{Path.Combine(destination, "noDate", file.Name)}");
                        continue;
                    }

                    if (parsedDateTime.HasValue)
                    {
                        File.Copy(filePath, newPath);
                        File.SetLastWriteTime(newPath, parsedDateTime.Value);
                        continue;
                    }

                    if (metadata != null)
                    {
                        File.Copy(filePath, newPath);
                        var date = metadata.CreationTime.GetDateTime();
                        File.SetLastWriteTime(newPath, date);
                    }
                }
                catch (Exception ex)
                {
                    FileHelper.WriteToCsv(destination, filePath, "Error", ex.Message);
                }
            }
        }
    }
}
