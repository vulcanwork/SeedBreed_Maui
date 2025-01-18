using SeedBreed.Data.Models;
using SeedBreed.Data.Transactions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SeedBreed.Core;
public class Api : INotifyPropertyChanged
{
    private string _message;
    private readonly Seedlings _seedlings;
    public IDataAccess DataAccess { get; set; }
    public Api(Seedlings seedlings)
    {
        var connection = "Data Source=192.168.69.101;Initial Catalog=SeedBreed;User ID=johnv;Password=Hyder1951;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        DataAccess = new SqlAccess(connection);
        _seedlings = seedlings;
    }
    //public Api(string connectionString) => DataAccess = new SqlAccess(connectionString);

    public string Message
    {
        get => _message; set
        {
            _message = value;
            NotifyPropertyChanged();
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public async Task DeleteItem<T>(T item) where T : IQuery => await DataAccess.DeleteItem(item);

    public async Task<List<GerminateModel>> GetGerminates()
    {

        return await DataAccess.GetItems(new GerminateModel());

    }
    public async Task<List<PlantModel>> GetPlants()
    {
        try
        {
            return await DataAccess.GetItems(new PlantModel());

        }
        catch (Exception e)
        {

            Message = $"Error GetPlants: {e.Message}";
            return new List<PlantModel>();
        }
    }
    public async Task<List<SourceModel>> GetSource()
    {
        try
        {
            return await DataAccess.GetItems(new SourceModel());

        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<List<SeedModel>> GetSeeds() => await DataAccess.GetItems(new SeedModel());

    public async Task SaveGerminate(GerminateModel germinate)
    {
        await DataAccess.SaveGerminate(germinate);
    }

    public async Task SavePlant(PlantModel plant)
    {

        plant.HarvestDate = plant.PlantId < 1 && plant.HarvestDate < DateTime.Parse("2000-1-1") ? DateTime.Parse("1-1-1800") : plant.HarvestDate;
        await DataAccess.SavePlant(plant);

    }

    public async Task SaveSeed(SeedModel seed)
    {

        await DataAccess.SaveSeed(seed);

    }

    public async Task SaveSource(SourceModel source) => await DataAccess.SaveSource(source);
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
