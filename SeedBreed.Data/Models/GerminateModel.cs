namespace SeedBreed.Data.Models;
public class GerminateModel : IQuery
{
    private int _germinateId;

    public string GetQuery => "SELECT * FROM Younglings.vw_Germinate";
    public string DeleteQuery => $"DELETE Younglings.Germinate WHERE GerminateId = {GerminateId}";
    public int CompareTo(GerminateModel other) => GerminateId.CompareTo(other.GerminateId);
    public int SelectedIndex { get; set; }
    public int GerminateId
    {
        get => _germinateId;
        set
        {
            _germinateId = value;
        }
    }
    public DateTime GerminationDate { get; set; } = DateTime.Now;
    public bool DidNotGerminate { get; set; }
    public int BatchId { get; set; }
    public int SourceId { get; set; }
    public string Source { get; set; } = string.Empty;
    public int SeedId { get; set; }
    public string Strain { get; set; } = string.Empty;
    public int OriginalQuantity { get; set; }
    public int QuantityRemaining { get; set; }
    public int SpawnId { get; set; }

}
