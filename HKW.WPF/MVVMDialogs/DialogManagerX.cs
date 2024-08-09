using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;
using HKW.HKWUtils.Extensions;
using HKW.WPF.Extensions;
using Microsoft.Extensions.Logging;

namespace HKW.WPF.MVVMDialogs;


/// <summary>
/// 对话框管理器
/// </summary>
public class DialogManagerX : DialogManager
{
    private static IEnumerable<Window> Windows => Application.Current.Windows.Cast<Window>();

    /// <inheritdoc cref="DialogManager.DialogManager(IViewLocator?, IDialogFactory?, ILogger{DialogManager}?, Dispatcher?)"/>
    public DialogManagerX(
        IViewLocator? viewLocator = null,
        IDialogFactory? dialogFactory = null,
        ILogger<DialogManager>? logger = null,
        Dispatcher? dispatcher = null
    )
        : base(viewLocator, dialogFactory, logger, dispatcher) { }

    /// <summary>
    /// 从视图模型寻找视图
    /// </summary>
    /// <param name="viewModel">视图模型</param>
    /// <returns>视图</returns>
    public override IView? FindViewByViewModel(INotifyPropertyChanged viewModel)
    {
        var view = ViewLocator.Locate(viewModel);
        // 如果不是页面, 则搜索窗口
        if (view.ViewType.InheritedFrom<Page>() is false)
            return Windows.FirstOrDefault(x => viewModel == x.DataContext).AsWrapper();

        // 如果是页面, 则搜索子元素
        return Windows
            .FirstOrDefault(x => x.FindVisualChild<Page>()?.DataContext == viewModel)
            .AsWrapper();
    }
}
