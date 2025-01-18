namespace SeedBreed.Data.Models;
public class SourceModel : IQuery
{
    public string GetQuery => "SELECT * FROM Seedlings.Source";
    public int SourceId { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string DeleteQuery => $"DELETE Seedlings.Source WHERE SourceId = {SourceId}";
}
//{
//    public int SourceId { get; set; }
//    public string Source { get; set; } = string.Empty;
//    public string Website { get; set; } = string.Empty;
//    public int Rating { get; set; }
//}
