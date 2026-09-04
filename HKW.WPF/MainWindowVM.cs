using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HanumanInstitute.MvvmDialogs;
using HKW.WPF.Converters;
using ReactiveUI;
using Splat;
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
namespace HKW.WPF;

internal partial class MainWindowVM : ReactiveObject
{
    private static readonly IDialogService _dialogService =
        Locator.Current.GetService<IDialogService>()!;

    public string Title { get; set; } = string.Empty;

    public List<string> Strs { get; } = new(Enumerable.Range(0, 10).Select(x => x.ToString()));

    public double Number { get; set; } = 1.1;
    public SolidColorBrush Brush { get; set; } = Brushes.White;

    //public Data Data { get; } = new();

    public MainWindowVM()
    {
        //var c = new CalculatorConverter();
        //var r = c.Convert(1, typeof(int), "+2", null);
        //EnumInfo<TestEnum>.Initialize();
        //_enums.MoveNext();
    }

    //private int _count = 0;

    private async void Next()
    {
        //Data.Refresh();
        //var vm = await _dialogService.ShowDialogAsyncX<ItemSelectionVM>(
        //    this,
        //    new(_enums, new List<TestEnum>())
        //    {
        //        Title = "114514",
        //        CaptionButtons = CaptionButtons.All,
        //        ResizeMode = ResizeMode.CanResizeWithGrip
        //    }
        //);
        //return;
        //Enum = _enums.Current;
        //_enums.MoveNext();
    }
}

internal enum TestEnum
{
    [Display(Name = "A_Name", ShortName = "A_ShortName", Description = "A_Description")]
    A,

    [Display(Name = "B_Name", ShortName = "B_ShortName", Description = "B_Description")]
    B,

    [Display(Name = "C_Name", ShortName = "C_ShortName", Description = "C_Description")]
    C,
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

//public class Data : INotifyPropertyChanged
//{
//    public string this[int i] => Random.Shared.Next().ToString();

//    public void Refresh()
//    {
//        PropertyChanged?.Invoke(this, new("Item[]"));
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;
//}
