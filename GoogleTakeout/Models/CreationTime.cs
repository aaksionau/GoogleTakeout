public class CreationTime
{
    public string Timestamp { get; set; }
    public string Formatted { get; set; }
    public DateTime GetDateTime()
    {
        if (Timestamp == null) return DateTime.MinValue;

        return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(long.Parse(Timestamp));
    }
}
