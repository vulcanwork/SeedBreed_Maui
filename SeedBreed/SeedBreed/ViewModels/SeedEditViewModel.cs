﻿
using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;

namespace SeedBreed.ViewModels;
public class SeedEditViewModel : EditViewBase
{
    public SeedEditViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
    {
        _navigationService = navigationService;

    }
    private SeedModel _selectedSeed;

    public SeedModel SelectedSeed
    {
        get { return _selectedSeed; }
        set { SetProperty(ref _selectedSeed, value); }
    }


    public override async Task ExecuteAddCommand()
    {
        try
        {
            await _api.SaveSeed(SelectedSeed);

        }
        catch (Exception e)
        {

            _api.Message = $"Error saving Seed: {e.Message}";
        }
        await GoBack();
    }
    public override async Task ExecuteDeleteCommand()
    {
        if (SelectedSeed.SeedId == 0)
        {
            await GoBack();
            return;
        }
        try
        {
            await _api.DeleteItem(SelectedSeed);
        }
        catch (Exception e)
        {
            _api.Message = $"Error deleting Batch: {e.Message}";
        }
        await GoBack();
    }
    private async Task GoBack()
    {
        await _navigationService.GoBackAsync();
    }
    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        if (parameters.ContainsKey("ModelId"))
        {
            var id = parameters.GetValue<int>("ModelId");
            SelectedSeed = id == 0 ? new SeedModel { SeedId = 0 } : Seedlings.Seeds.FirstOrDefault(x => x.SeedId == id);
        }
        else
        {
            SelectedSeed = new SeedModel { SeedId = 0 };
        }
        //SelectedSeed = Seedlings.Seeds.FirstOrDefault(x => x.SeedId == navigationContext.Count);
    }
}
