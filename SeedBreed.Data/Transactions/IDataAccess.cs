using SeedBreed.Data.Models;

namespace SeedBreed.Data.Transactions;

public interface IDataAccess
{
    string ConnectionString { get; set; }
    Task DeleteItem<T>(T item) where T : IQuery;
    //Task<List<GerminateModel>> GetGerminates();
    //Task<List<SeedModel>> GetSeeds();
    //Task<List<SourceModel>> GetSource();
    Task SaveGerminate(GerminateModel germinate);
    Task<List<T>> GetItems<T>(T item) where T : IQuery;
    Task SaveSeed(SeedModel seed);
    Task SaveSource(SourceModel source);
    //Task<List<PlantModel>> GetPlants();
    Task SavePlant(PlantModel plant);
}