namespace SeedBreed.Data.Models;
public class SeedModel : IQuery
{
    public string GetQuery => "SELECT * FROM Seedlings.Seed";
    public int SeedId { get; set; }
    public string Strain { get; set; } = "";
    public int IndicaPercentage { get; set; }
    public int SativaPercentage { get; set; }
    public int ThcPercentage { get; set; }
    public bool IsMedical { get; set; }
    public bool IsFeminized { get; set; }
    public string DeleteQuery => $"DELETE Seedlings.Seed WHERE SeedId = {SeedId}";


}

