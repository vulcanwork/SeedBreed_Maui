using SeedBreed.Core;

namespace SeedBreed.Mvvm;
public class ListViewBase : BindableBase, INavigationAware
{

    protected Api _api;
    protected INavigationService _navigationService;
    public ListViewBase(Api api, Seedlings seedlings, INavigationService navigationService)
    {
        AddCommand = new DelegateCommand(async () => await ExecuteAddCommand());
        _api = api;
        _navigationService = navigationService;
        Seedlings = seedlings;
    }
    public virtual async Task ExecuteAddCommand()
    {

    }

    public Seedlings Seedlings { get; set; }
    public DelegateCommand AddCommand { get; set; }
    public DelegateCommand EditCommand { get; set; }
    public virtual async Task GetData()
    {

    }

    public virtual void OnNavigatedTo(INavigationParameters parameters) => _ = GetData();

    public void OnNavigatedFrom(INavigationParameters parameters)
    {

    }
}
