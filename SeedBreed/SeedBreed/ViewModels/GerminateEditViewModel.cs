using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;
using System.Collections.ObjectModel;

namespace SeedBreed.ViewModels
{
    public class GerminateEditViewModel : EditViewBase
    {
        private ObservableCollection<ComboSelector> _seeds = new();
        private ObservableCollection<ComboSelector> _sources = new();
        private ComboSelector _selectedSeed = new();
        private ComboSelector _selectedSource = new();
        private GerminateModel _selectedGerminate = new();
        public GerminateEditViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
        {
        }
        public GerminateModel SelectedGerminate
        {
            get => _selectedGerminate;
            set => SetProperty(ref _selectedGerminate, value);
        }
        public override async Task ExecuteAddCommand()
        {
            SelectedGerminate.SeedId = SelectedSeed.Id;
            SelectedGerminate.SourceId = SelectedSource.Id;
            try
            {
                await _api.SaveGerminate(SelectedGerminate);
            }
            catch (Exception e)
            {
                _api.Message = $"Error saving Germinate: {e.Message}";
            }
            await GoBack();
        }
        public override async Task ExecuteDeleteCommand()
        {
            if (SelectedGerminate == null || SelectedGerminate.GerminateId == 0)
            {
                await GoBack();
                return;
            }
            try
            {
                await _api.DeleteItem(SelectedGerminate);
            }
            catch (Exception e)
            {
                _api.Message = $"Error deleting germinate: {e.Message}";
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
                SelectedGerminate = id == 0 ? new GerminateModel { GerminateId = 0 } : Seedlings.Germinates.FirstOrDefault(x => x.GerminateId == id);
            }
            else
            {
                SelectedGerminate = new GerminateModel { GerminateId = 0 };
            }
            _ = BuildComboSelectors();
        }



        private async Task BuildComboSelectors()
        {
            Seedlings.Seeds = await _api.GetSeeds();
            Seedlings.Source = await _api.GetSource();
            Seeds.Add(new ComboSelector { Id = 0, Name = "Select Seed" });
            foreach (var seed in Seedlings.Seeds)
            {
                Seeds.Add(new ComboSelector { Id = seed.SeedId, Name = seed.Strain });
            }
            Sources.Add(new ComboSelector { Id = 0, Name = "Select Source" });
            foreach (var source in Seedlings.Source)
            {
                Sources.Add(new ComboSelector { Id = source.SourceId, Name = source.Source });
            }
            if (SelectedGerminate.SeedId > 0)
            {
                SelectedSeed = Seeds.FirstOrDefault(x => x.Id == SelectedGerminate.SeedId);
                SelectedSource = Sources.FirstOrDefault(x => x.Id == SelectedGerminate.SourceId);
            }
        }

        public ComboSelector SelectedSource
        {
            get { return _selectedSource; }
            set { SetProperty(ref _selectedSource, value); }
        }
        public ComboSelector SelectedSeed
        {
            get { return _selectedSeed; }
            set { SetProperty(ref _selectedSeed, value); }
        }
        public ObservableCollection<ComboSelector> Sources
        {
            get { return _sources; }
            set { SetProperty(ref _sources, value); }
        }
        public ObservableCollection<ComboSelector> Seeds
        {
            get { return _seeds; }
            set { SetProperty(ref _seeds, value); }
        }



    }

}

