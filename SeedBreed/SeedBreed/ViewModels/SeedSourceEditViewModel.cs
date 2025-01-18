using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;

namespace SeedBreed.ViewModels
{
    public class SeedSourceEditViewModel : EditViewBase
    {
        private SourceModel _selectedSource = new();
        public SeedSourceEditViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
        {
        }
        public SourceModel SelectedSource
        {
            get => _selectedSource;
            set => SetProperty(ref _selectedSource, value);
        }
        public override async Task ExecuteAddCommand()
        {
            try
            {
                await _api.SaveSource(SelectedSource);
            }
            catch (Exception e)
            {
                _api.Message = $"Error saving Source: {e.Message}";
            }
            await GoBack();
        }
        public override async Task ExecuteDeleteCommand()
        {
            if (SelectedSource == null || SelectedSource.SourceId == 0)
            {
                await GoBack();
                return;
            }
            try
            {
                await _api.DeleteItem(SelectedSource);
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
                SelectedSource = id == 0 ? new SourceModel { SourceId = 0 } : Seedlings.Source.FirstOrDefault(x => x.SourceId == id);
            }
            else
            {
                SelectedSource = new SourceModel { SourceId = 0 };
            }
            //SelectedSeed = Seedlings.Seeds.FirstOrDefault(x => x.SeedId == navigationContext.Count);
        }

    }
}
