using Hits.Services.Dapper;
using SeedBreed.Data.Models;

namespace SeedBreed.Data.Transactions;
public class SqlAccess : IDataAccess
{
    public string ConnectionString { get; set; }
    public SqlAccess(string connectionString) => ConnectionString = connectionString;
    public async Task DeleteItem<T>(T item) where T : IQuery =>
        await Transact.QueryAsync(item.DeleteQuery, ConnectionString);
    public async Task<List<T>> GetItems<T>(T item) where T : IQuery =>
        await Transact.QueryDataAsync<T>(item.GetQuery, ConnectionString);
    //public async Task<List<GerminateModel>> GetGerminates() =>
    //    await Transact.QueryDataAsync<GerminateModel>(GerminateModel.GetQuery, ConnectionString);
    //public async Task<List<PlantModel>> GetPlants() =>
    //    await Transact.QueryDataAsync<PlantModel>(PlantModel.GetQuery, ConnectionString);
    //public async Task<List<SourceModel>> GetSource() =>
    //    await Transact.QueryDataAsync<SourceModel>(SourceModel.GetQuery, ConnectionString);
    //public async Task<List<SeedModel>> GetSeeds() =>
    //    await Transact.QueryDataAsync<SeedModel>(SeedModel.GetQuery, ConnectionString);
    public async Task SaveGerminate(GerminateModel germinate) =>
        await ConvertParameters.WriteDataAsync<GerminateModel>(germinate, "Younglings.usp_SaveGerminate", ConnectionString);
    public async Task SaveSeed(SeedModel seed) =>
        await ConvertParameters.WriteDataAsync<SeedModel>(seed, "Seedlings.usp_SaveSeeds", ConnectionString);
    public async Task SaveSource(SourceModel source) =>
        await ConvertParameters.WriteDataAsync<SourceModel>(source, "Seedlings.usp_SaveSource", ConnectionString);
    public async Task SavePlant(PlantModel plant) =>
        await ConvertParameters.WriteDataAsync<PlantModel>(plant, "Padawans.usp_SavePlant", ConnectionString);
}
