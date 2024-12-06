using System.Reactive.Disposables;
using System.Windows;
using HKW.WPF.Converters;
using HKW.WPF.Extensions;
using ReactiveUI;

namespace HKW.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
internal partial class MainWindow : Window, IViewFor<MainWindowVM>
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowVM();
        //this.WhenActivated(d =>
        //{
        //    this.Bind(
        //            ViewModel,
        //            x => x.Brush,
        //            x => x.ColorPicker_1.SelectedColor,
        //            vm => vm?.Color,
        //            v => new(v!.Value)
        //        )
        //        .DisposeWith(d);
        //});
        //window.MaskClose(this);
    }

    /// <inheritdoc/>
    public MainWindowVM? ViewModel
    {
        get => (MainWindowVM)DataContext;
        set => DataContext = value;
    }

    /// <inheritdoc/>
    object? IViewFor.ViewModel
    {
        get => DataContext;
        set => DataContext = value;
    }

    //public Window window =
    //    new()
    //    {
    //        Width = 500,
    //        Height = 400,
    //        WindowStartupLocation = WindowStartupLocation.CenterScreen
    //    };

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //MessageBox.Show("Hello, World!", "", MessageBoxButton.YesNo);
        //if (window.IsLoaded)
        //    window.SetLocationToCenter();
        //window.ShowOrActivate();
    }

    private void Button_1_Click(object sender, RoutedEventArgs e)
    {
        //window.CloseX();
    }
}
