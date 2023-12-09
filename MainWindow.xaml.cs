namespace HKW.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
internal partial class MainWindow : Window
{
    public MainWindowVM ViewModel => (MainWindowVM)DataContext;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowVM();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //ListBox_Main.SelectedItem = null;
    }
}
