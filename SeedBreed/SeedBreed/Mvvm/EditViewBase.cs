using SeedBreed.Core;

namespace SeedBreed.Mvvm;
public class EditViewBase : BindableBase, INavigationAware
{
    protected Api _api;
    protected INavigationService _navigationService;


    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {

    }

    public EditViewBase(Api api, Seedlings seedlings, INavigationService navigationService)
    {
        SaveCommand = new DelegateCommand(async () => await ExecuteAddCommand());
        DeleteCommand = new DelegateCommand(async () => await ExecuteDeleteCommand());
        _api = api;
        _navigationService = navigationService;
        Seedlings = seedlings;
    }
    public virtual async Task ExecuteDeleteCommand()
    {

    }

    public Seedlings Seedlings { get; set; }
    public DelegateCommand SaveCommand { get; set; }

    public DelegateCommand DeleteCommand { get; set; }


    public virtual async Task ExecuteAddCommand()
    {

    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
    }
}
