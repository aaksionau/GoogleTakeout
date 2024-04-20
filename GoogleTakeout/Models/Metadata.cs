namespace GoogleTakeout.Models;

public class Metadata
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageViews { get; set; }
    public CreationTime CreationTime { get; set; }
    public PhotoTakenTime PhotoTakenTime { get; set; }
    public GeoData GeoData { get; set; }
    public Geodataexif GeoDataExif { get; set; }
    public Person[] People { get; set; }
    public string Url { get; set; }
}