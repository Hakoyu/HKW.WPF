using HKW.HKWUtils.Observable;

namespace HKW.WPF;

internal class MainWindowVM : ObservableObjectX<MainWindowVM>
{
    public MainWindowVM() { }
}

//internal class TestVM : ObservableClass<TestVM>
//{
//    private bool _isChecked;
//    public bool IsChecked
//    {
//        get => _isChecked;
//        set => SetProperty(ref _isChecked, value);
//    }

//    private string _content = string.Empty;
//    public string Content
//    {
//        get => _content;
//        set => SetProperty(ref _content, value);
//    }
//}
