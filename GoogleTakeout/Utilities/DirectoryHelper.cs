namespace GoogleTakeout.Utilities
{
    public static class DirectoryHelper
    {
        public static List<string> GetAllFilesWithExtension(string source, List<string> filesList, List<string> extensions)
        {
            var files = Directory.GetFiles(source, "*", SearchOption.TopDirectoryOnly).ToList();
            var filesToExport = files.Where(x => extensions.Any(e => x.EndsWith(e, StringComparison.OrdinalIgnoreCase)));
            filesList.AddRange(filesToExport);

            var folders = Directory.GetDirectories(source);

            foreach (var folder in folders)
            {
                GetAllFilesWithExtension(folder, filesList, extensions);
            }

            return filesList;
        }
    }
}
