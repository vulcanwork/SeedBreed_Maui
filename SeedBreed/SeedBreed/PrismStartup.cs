using SeedBreed.Core;
using SeedBreed.ViewModels;
using SeedBreed.Views;

namespace SeedBreed;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .CreateWindow("NavigationPage/MainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>()
            .RegisterForNavigation<SeedView, SeedViewModel>()
            .RegisterForNavigation<SeedEditView, SeedEditViewModel>()
            .RegisterForNavigation<SeedSourceView, SeedSourceViewModel>()
            .RegisterForNavigation<SeedSourceEditView, SeedSourceEditViewModel>()
            .RegisterForNavigation<GerminateView, GerminateViewModel>()
            .RegisterForNavigation<GerminateEditView, GerminateEditViewModel>()
            .RegisterForNavigation<PlantView, PlantViewModel>()
            .RegisterForNavigation<PlantEditView, PlantEditViewModel>()
            .RegisterForNavigation<CalculatorView, CalculatorViewModel>()
            .RegisterSingleton<Api>()
            .RegisterSingleton<Seedlings>()
            .RegisterInstance(SemanticScreenReader.Default);
    }
}
