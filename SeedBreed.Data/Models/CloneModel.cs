namespace SeedBreed.Data.Models;
public class CloneModel : IQuery
{
    public int CloneId { get; set; }
    public DateTime CloneDate { get; set; }
    public decimal CloneRate { get; set; }
    public int Quantity { get; set; }

    public string GetQuery => throw new NotImplementedException();

    public string DeleteQuery => throw new NotImplementedException();
}
