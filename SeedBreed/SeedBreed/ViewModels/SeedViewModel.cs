using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;

namespace SeedBreed.ViewModels;

public class SeedViewModel : ListViewBase
{
    public SeedViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService) => _ = GetData();

    private SeedModel _selectedSeed = new();
    public override async Task GetData() => Seedlings.Seeds = await _api.GetSeeds();
    public override async Task ExecuteAddCommand() => await NavigateToEditView(true);

    public SeedModel SelectedSeed
    {
        get => _selectedSeed;
        set
        {
            SetProperty(ref _selectedSeed, value);
            if (_selectedSeed == null || _selectedSeed.SeedId == 0)
            {
                return;
            }
            _ = NavigateToEditView(false);
        }
    }

    private async Task NavigateToEditView(bool isNew)
    {
        var id = isNew ? 0 : SelectedSeed.SeedId;
        await _navigationService.NavigateAsync("SeedEditView", new NavigationParameters
        {
            {"ModelId" , id }
        });
    }
}

