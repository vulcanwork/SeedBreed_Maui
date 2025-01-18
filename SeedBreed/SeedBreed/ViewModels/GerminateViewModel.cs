using SeedBreed.Core;
using SeedBreed.Data.Models;
using SeedBreed.Mvvm;

namespace SeedBreed.ViewModels
{
    public class GerminateViewModel : ListViewBase
    {

        public GerminateViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
        {
            _ = GetData();
        }
        private GerminateModel _selectedGerminate = new();
        public override async Task GetData() => Seedlings.Germinates = await _api.GetGerminates();
        public override async Task ExecuteAddCommand() => await NavigateToEditView(true);

        public GerminateModel SelectedGerminate
        {
            get => _selectedGerminate;
            set
            {
                SetProperty(ref _selectedGerminate, value);
                if (_selectedGerminate == null || _selectedGerminate.GerminateId == 0)
                {
                    return;
                }
                _ = NavigateToEditView(false);
            }
        }

        private async Task NavigateToEditView(bool isNew)
        {
            var id = isNew ? 0 : SelectedGerminate.GerminateId;
            await _navigationService.NavigateAsync("GerminateEditView", new NavigationParameters
        {
            {"ModelId" , id }
        });
        }

    }
}
