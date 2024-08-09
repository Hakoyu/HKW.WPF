using System.Windows;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;
using ReactiveUI;
using Splat;

namespace HKW.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
internal partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var build = Locator.CurrentMutable;

        build.RegisterLazySingleton<IDialogService>(
            () =>
                new DialogService(
                    new DialogManager(viewLocator: new ViewLocator()),
                    viewModelFactory: x => Locator.Current.GetService(x)
                )
        );

        build.Register<MainWindowVM>(() => new());

        build.InitializeSplat();
        build.InitializeReactiveUI();

        //SplatRegistrations.Register<MainWindowViewModel>();
        //SplatRegistrations.Register<CurrentTimeDialogViewModel>();
        //SplatRegistrations.SetupIOC();
    }
}
