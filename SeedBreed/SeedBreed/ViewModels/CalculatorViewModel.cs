using SeedBreed.Core;
using SeedBreed.Mvvm;
using System.Collections.ObjectModel;

namespace SeedBreed.ViewModels;
internal class CalculatorViewModel : EditViewBase
{
    public CalculatorViewModel(Api api, Seedlings seedlings, INavigationService navigationService) : base(api, seedlings, navigationService)
    {
        Calculator = new();
        VesicleString = new();
        foreach (var vesicle in Calculator.Vesicles)
        {
            VesicleString.Add(vesicle.Key);
        }
        SelectedIndex = 0;
    }
    private ObservableCollection<string> _vesicleString;
    private string _selectedVesicle;
    private decimal _tempDecimal;
    private InfusionCalculator _calculator;
    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set { SetProperty(ref _selectedIndex, value); }
    }
    public string SelectedVesicle
    {
        get => _selectedVesicle;
        set
        {
            if (value is null) return;
            SetProperty(ref _selectedVesicle, value);
            var t = Calculator.Vesicles[_selectedVesicle];
            Calculator.VesicleYield = t;
        }
    }
    public InfusionCalculator Calculator
    {
        get => _calculator;
        set => SetProperty(ref _calculator, value);
    }
    public ObservableCollection<string> VesicleString
    {
        get => _vesicleString;
        set => SetProperty(ref _vesicleString, value);
    }
}
