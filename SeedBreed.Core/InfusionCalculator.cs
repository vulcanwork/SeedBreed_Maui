using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeedBreed.Core;
public class InfusionCalculator : INotifyPropertyChanged
{
    private decimal _postDecarboxylationYield = 0.877m;
    private decimal _vesicleYield;
    private decimal _mgThcInfused;
    private int _gramsCannabis = 4000;
    private int _percentThc = 80;
    private int _percentCbd = 10;
    private int _mlVesicle = 20;
    private int _mlInfusedOilUsed = 6;
    private int _totalServings = 64;
    private decimal _mgCbdInfused;
    private decimal _mgCbdTotal;
    private decimal _mgThcTotal;
    private decimal _mgCbdPerServing;
    private decimal _mgThcPerServing;
    private decimal _mgPerMlCbd;
    private decimal _mgCbdInfused1;
    private decimal _mgPerMlThc;
    private bool _calculating;
    public int MgCannabis
    {
        get => _gramsCannabis;
        set
        {
            _gramsCannabis = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgThcInfused = CalculateInfusedThc();
            //MgPerMlThc = CalculateMgMlThc();
        }
    }
    public int PercentThc
    {
        get => _percentThc;
        set
        {
            _percentThc = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgThcInfused = CalculateInfusedThc();
        }
    }
    public int PercentCbd
    {
        get => _percentCbd;
        set
        {
            _percentCbd = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgCbdInfused = CalculateInfusedCbd();
        }
    }
    public int MlVesicle
    {
        get => _mlVesicle;
        set
        {
            _mlVesicle = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgPerMlThc = CalculateMgMlThc();
            //MgPerMlCbd = CalculateMgMlCbd();

        }
    }
    public decimal VesicleYield
    {
        get => _vesicleYield;
        set
        {
            _vesicleYield = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgThcInfused = CalculateInfusedThc();
            //MgCbdInfused = CalculateInfusedCbd();
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public Dictionary<string, decimal> Vesicles => new Dictionary<string, decimal>
        {
            { "Avocado", .787m },
            { "Coconut", .858m },
            { "Olive", .823m },
            { "Butter", .889m },
            { "190 Proof", .950m },
            { "Bacon Fat", .667m },
            {"Grapeseed",.810m },
            {"MTC",.900m },
            {"Walnut",.830m }
        };
    public decimal MgThcInfused
    {
        get => _mgThcInfused;
        set
        {
            _mgThcInfused = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgPerMlThc = CalculateMgMlThc();
        }
    }

    public decimal MgPerMlThc
    {
        get => _mgPerMlThc;
        set
        {
            _mgPerMlThc = value;
            NotifyPropertyChanged();
        }
    }

    public decimal MgCbdInfused
    {
        get => _mgCbdInfused1;
        set
        {
            _mgCbdInfused1 = value;
            NotifyPropertyChanged();
            if (!_calculating)
                CalculateInfusion();
            //MgPerMlCbd = CalculateMgMlCbd();
        }
    }

    public decimal MgPerMlCbd
    {
        get => _mgPerMlCbd;
        set
        {
            _mgPerMlCbd = value;
            NotifyPropertyChanged();
        }
    }

    public decimal MgThcPerServing
    {
        get => _mgThcPerServing;
        set
        {
            _mgThcPerServing = value;
            NotifyPropertyChanged();
        }
    }

    public decimal MgCbdPerServing
    {
        get => _mgCbdPerServing;
        set
        {
            _mgCbdPerServing = value;
            NotifyPropertyChanged();
        }
    }

    public decimal MgThcPerRecipe
    {
        get => _mgThcTotal;
        set
        {
            _mgThcTotal = value;
            NotifyPropertyChanged();
        }
    }

    public decimal MgCbdPerRecipe
    {
        get => _mgCbdTotal;
        set
        {
            _mgCbdTotal = value;
            NotifyPropertyChanged();
        }
    }
    public int MlInfusedOilUsed
    {
        get => _mlInfusedOilUsed;
        set
        {
            _mlInfusedOilUsed = value;
            NotifyPropertyChanged();
            CalculateServings();
        }
    }


    public int TotalServings
    {
        get => _totalServings; set
        {
            _totalServings = value;
            NotifyPropertyChanged();
            CalculateServings();
        }
    }

    public void CalculateServings()
    {

        MgThcPerRecipe = CalculateThcTotal();
        MgCbdPerRecipe = CalculateCbdTotal();
        MgThcPerServing = CalculateThcServing();
        MgCbdPerServing = CalculateCbdServing();


    }
    private void CalculateInfusion()
    {
        _calculating = true;

        try
        {
            MgThcInfused = CalculateInfusedThc();
            MgCbdInfused = CalculateInfusedCbd();
            MgPerMlThc = CalculateMgMlThc();
            MgPerMlCbd = CalculateMgMlCbd();
            CalculateServings();
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            _calculating = false;

        }
    }

    private decimal CalculateInfusedThc() => (MgCannabis * PercentThc / 100 * _postDecarboxylationYield * VesicleYield);

    private decimal CalculateMgMlThc() => MgThcInfused == 0 || MlVesicle == 0 ? 0 : MgThcInfused / MlVesicle;

    private decimal CalculateInfusedCbd() => (MgCannabis * PercentCbd / 100 * _postDecarboxylationYield * VesicleYield);

    private decimal CalculateMgMlCbd() => MgCbdInfused == 0 || MlVesicle == 0 ? 0 : MgCbdInfused / MlVesicle;

    private decimal CalculateThcServing() => TotalServings < 1 ? 0 : MgThcPerRecipe / TotalServings;

    private decimal CalculateCbdServing() => TotalServings < 1 ? 0 : MgCbdPerRecipe / TotalServings;

    private decimal CalculateThcTotal() => MgPerMlThc * MlInfusedOilUsed;

    private decimal CalculateCbdTotal() => MgPerMlCbd * MlInfusedOilUsed;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}
