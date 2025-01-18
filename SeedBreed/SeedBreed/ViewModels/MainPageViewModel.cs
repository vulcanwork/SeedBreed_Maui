namespace SeedBreed.ViewModels;

public class MainPageViewModel : BindableBase
{
    private ISemanticScreenReader _screenReader { get; }
    //private int _count;
    private readonly INavigationService _navigation;
    public MainPageViewModel(ISemanticScreenReader screenReader, INavigationService navigation)
    {
        _screenReader = screenReader;
        _navigation = navigation;
        CountCommand = new DelegateCommand(OnCountCommandExecuted);
    }

    public string Title => "Main Page";

    private string _text = "Click me";
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public DelegateCommand CountCommand { get; }

    private void OnCountCommandExecuted()
    {
        _ = _navigation.NavigateAsync("SeedView");

    }
}
