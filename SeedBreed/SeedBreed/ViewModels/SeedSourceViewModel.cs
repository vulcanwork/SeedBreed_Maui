using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;

namespace SeedBreed.ViewModels;

class SeedSourceViewModel : ListViewBase
{
    private SourceModel _selectedSource = new();


    public SeedSourceViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService) => _ = GetData();



    public override async Task GetData() => Seedlings.Source = await _api.GetSource();
    public override async Task ExecuteAddCommand() => await NavigateToEditView(true);
    //{
    //    if (SelectedSource == null || SelectedSource.SourceId == 0)
    //    {
    //        _api.Message = "No Batch selected";
    //        return;
    //    }
    //    try
    //    {
    //        await _api.DeleteItem(SelectedSource);
    //    }
    //    catch (Exception e)
    //    {
    //        _api.Message = $"Error deleting Batch: {e.Message}";
    //    }
    //    await GetData();
    //}


    public SourceModel SelectedSource
    {
        get => _selectedSource;
        set
        {
            SetProperty(ref _selectedSource, value);
            if (_selectedSource?.SourceId == 0)
            {
                return;
            }
            else if (_selectedSource is null)
            {
                return;
            }
            _ = NavigateToEditView(false);

        }
    }
    private async Task NavigateToEditView(bool isNew)
    {
        var id = isNew ? 0 : SelectedSource.SourceId;
        await _navigationService.NavigateAsync("SeedSourceEditView", new NavigationParameters
        {
            {"ModelId" , id }
        });
    }
}
