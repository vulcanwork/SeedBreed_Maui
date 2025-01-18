
using System;
using Prism.Mvvm;
using Prism.Navigation.Regions;

namespace SeedBreed.Mvvm;
public class RegionViewModelBase : BindableBase, IConfirmNavigationRequest
{
    protected IRegionManager RegionManager { get; private set; }

    public RegionViewModelBase(IRegionManager regionManager) => RegionManager = regionManager;

    public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

    public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {

    }
}
