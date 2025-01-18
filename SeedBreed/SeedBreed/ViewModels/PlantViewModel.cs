using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;

namespace SeedBreed.ViewModels
{
    class PlantViewModel : ListViewBase
    {
        public PlantViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
        {
            _ = GetData();
        }
        private PlantModel _selectedPlant = new();
        public override async Task GetData() => Seedlings.Plants = await _api.GetPlants();
        public override async Task ExecuteAddCommand() => await NavigateToEditView(true);

        public PlantModel SelectedPlant
        {
            get => _selectedPlant;
            set
            {
                SetProperty(ref _selectedPlant, value);
                if (_selectedPlant == null || _selectedPlant.GerminateId == 0)
                {
                    return;
                }
                _ = NavigateToEditView(false);
            }
        }

        private async Task NavigateToEditView(bool isNew)
        {
            var id = isNew ? 0 : SelectedPlant.PlantId;
            await _navigationService.NavigateAsync("PlantEditView", new NavigationParameters
        {
            {"ModelId" , id }
        });
        }
    }
}
