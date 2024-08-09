using System.Windows;
using HKW.WPF.Extensions;

namespace HKW.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
internal partial class MainWindow : Window
{
    public MainWindowVM ViewModel => (MainWindowVM)DataContext;

    public Window window =
        new()
        {
            Width = 500,
            Height = 400,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowVM();
        window.MaskClose(this);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Hello, World!", "", MessageBoxButton.YesNo);
        //window.SetLocationToCenter(this);
        //window.ShowOrActivate(this);
    }

    private void Button_1_Click(object sender, RoutedEventArgs e)
    {
        window.CloseX();
    }
}
