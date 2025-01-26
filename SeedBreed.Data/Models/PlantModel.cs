namespace SeedBreed.Data.Models;
public class PlantModel : IQuery
{
    public string GetQuery => "SELECT * FROM Padawans.vw_Plants";
    public int PlantId { get; set; }
    public int GerminateId { get; set; }
    public int CloneId { get; set; }
    public DateTime GerminationDate { get; set; }
    public DateTime PlantDate { get; set; } = DateTime.Now;
    public DateTime HarvestDate { get; set; } = DateTime.Now;
    public int HarvestInGrams { get; set; }
    public bool Gifted { get; set; }
    public bool IsMotherPlant { get; set; }
    public int MediaTypeId { get; set; }
    public int MotherPlantId { get; set; }
    public string Strain { get; set; } = string.Empty;
    public string DeleteQuery => $"DELETE Padawans.Plant WHERE PlantId = {PlantId}";
}