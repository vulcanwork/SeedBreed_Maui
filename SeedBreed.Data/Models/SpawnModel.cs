namespace SeedBreed.Data.Models;
internal class SpawnModel
{
    public int SpawnId { get; set; }
    public int PlantId { get; set; }
    public int PollenId { get; set; }
    public bool Hybrid { get; set; }
    public bool Direct { get; set; }
    public bool Inbreed { get; set; }

}
