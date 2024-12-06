using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using DynamicData.Binding;
using HanumanInstitute.MvvmDialogs;
using HKW.HKWReactiveUI;
using HKW.HKWUtils;
using HKW.HKWUtils.Collections;
using HKW.HKWUtils.Extensions;
using HKW.HKWUtils.Observable;
using HKW.MVVMDialogs;
using HKW.WPF.Converters;
using HKW.WPF.MVVMDialogs;
using ReactiveUI;
using Splat;

namespace HKW.WPF;

internal partial class MainWindowVM : ReactiveObjectX
{
    private static readonly IDialogService _dialogService =
        Locator.Current.GetService<IDialogService>()!;

    [ReactiveProperty]
    public string Title { get; set; } = string.Empty;

    private readonly CyclicList<TestEnum> _enums =
        new(EnumInfo<TestEnum>.Values) { AutoReset = true };

    [ReactiveProperty]
    public double Number { get; set; } = 1.1;

    [ReactiveProperty]
    public SolidColorBrush Brush { get; set; } = Brushes.White;

    partial void OnBrushChanged(SolidColorBrush oldValue, SolidColorBrush newValue)
    {
        return;
    }

    public MainWindowVM()
    {
        //var c = new CalculatorConverter();
        //var r = c.Convert(1, typeof(int), "+2", null);
        //EnumInfo<TestEnum>.Initialize();
        //_enums.MoveNext();
    }

    private int _count = 0;

    [ReactiveCommand]
    private async void Next()
    {
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
