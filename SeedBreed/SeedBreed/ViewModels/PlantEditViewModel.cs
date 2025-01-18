using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;
using System.Collections.ObjectModel;

namespace SeedBreed.ViewModels
{
    class PlantEditViewModel : EditViewBase
    {
        private ObservableCollection<ComboSelector> _Strains = new();
        private ComboSelector _selectedStrain = new();
        private PlantModel _selectedPlant = new();
        private int _StrainsMotherDivider;
        private int _selectedIndex;
        public PlantEditViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
        {
        }
        public PlantModel SelectedPlant
        {
            get => _selectedPlant;
            set => SetProperty(ref _selectedPlant, value);
        }
        public override async Task ExecuteAddCommand()
        {
            if (SelectedIndex > _StrainsMotherDivider)
            {
                SelectedPlant.MotherPlantId = SelectedStrain.Id;
            }
            else
            {
                SelectedPlant.GerminateId = SelectedStrain.Id;
            }
            try
            {
                await _api.SavePlant(SelectedPlant);
            }
            catch (Exception e)
            {
                _api.Message = $"Error saving Strains: {e.Message}";
            }
            await GoBack();
        }
        public override async Task ExecuteDeleteCommand()
        {
            if (SelectedPlant == null || SelectedPlant.PlantId == 0)
            {
                await GoBack();
                return;
            }
            try
            {
                await _api.DeleteItem(SelectedPlant);
            }
            catch (Exception e)
            {
                _api.Message = $"Error deleting Strains: {e.Message}";
            }
            await GoBack();
        }
        private async Task GoBack()
        {
            await _navigationService.GoBackAsync(new NavigationParameters());
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("ModelId"))
            {
                var id = parameters.GetValue<int>("ModelId");
                SelectedPlant = id == 0 ? new PlantModel { PlantId = 0 } : Seedlings.Plants.FirstOrDefault(x => x.PlantId == id);
            }
            else
            {
                SelectedPlant = new PlantModel { PlantId = 0 };
            }
            _ = BuildComboSelectors();
        }



        private async Task BuildComboSelectors()
        {
            Seedlings.Germinates = await _api.GetGerminates();
            Strains.Add(new ComboSelector { Id = 0, Name = "Select Strains" });
            foreach (var germinate in Seedlings.Germinates)
            {
                Strains.Add(new ComboSelector { Id = germinate.GerminateId, Name = $"{germinate.Strain} on {germinate.GerminationDate}", IsFirstHalf = true });
                _StrainsMotherDivider++;
            }
            Strains.Add(new ComboSelector { Id = -1, Name = "Select Mother" });
            foreach (var plant in Seedlings.Plants)
            {
                if (plant.IsMotherPlant)
                    Strains.Add(new ComboSelector { Id = plant.PlantId, Name = $"{plant.Strain}", IsFirstHalf = false });
            }
            var list = new List<ComboSelector>();
            if (SelectedPlant.GerminateId > 0)
            {
                list.AddRange(Strains.Where(x => x.IsFirstHalf));
                SelectedStrain = list.FirstOrDefault(x => x.Id == SelectedPlant.GerminateId);
            }
            else if (SelectedPlant.MotherPlantId > 0)
            {
                list.AddRange(Strains.Where(x => !x.IsFirstHalf));
                SelectedStrain = list.FirstOrDefault(x => x.Id == SelectedPlant.MotherPlantId);
            }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }
        public ComboSelector SelectedStrain
        {
            get { return _selectedStrain; }
            set { SetProperty(ref _selectedStrain, value); }
        }
        public ObservableCollection<ComboSelector> Strains
        {
            get { return _Strains; }
            set { SetProperty(ref _Strains, value); }
        }

    }
}
