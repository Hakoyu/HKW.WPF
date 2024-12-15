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
        if (ViewLocator is not StrongViewLocatorX viewLocatorX)
            return null;
        (var pageDefinition, var windowDefinition) = viewLocatorX.LocateX(viewModel);
        // 如果同时注册了页面和窗口,则先查找窗口然后查找页面
        if (pageDefinition is not null && windowDefinition is not null)
        {
            var result = Windows.FirstOrDefault(x => x.IsActive && x.DataContext == viewModel);

            if (result is null)
            {
                return Windows
                    .FirstOrDefault(x =>
                    {
                        if (x is IPageLocator pageView)
                        {
                            if (
                                pageView.LocatePageByType?.Invoke(pageDefinition.Value.ViewType)
                                is not FrameworkElement page
                            )
                                return false;
                            return page.DataContext == viewModel;
                        }
                        return x.FindVisualChild<Page>()?.DataContext == viewModel;
                    })
                    .AsWrapper();
            }
            else
            {
                return result.AsWrapper();
            }
        }
        // 如果是页面, 则搜索子元素
        else if (pageDefinition is not null)
        {
            return Windows
                .FirstOrDefault(x =>
                {
                    if (x is IPageLocator pageView)
                    {
                        if (
                            pageView.LocatePageByType?.Invoke(pageDefinition.Value.ViewType)
                            is not FrameworkElement page
                        )
                            return false;
                        return page.DataContext == viewModel;
                    }
                    return x.FindVisualChild<Page>()?.DataContext == viewModel;
                })
                .AsWrapper();
        }
        else if (windowDefinition is not null)
        {
            // 如果不是页面, 则搜索窗口
            return Windows.FirstOrDefault(x => x.DataContext == viewModel).AsWrapper();
        }
        else
        {
            throw new NotImplementedException(
                "No view was registered for view model " + viewModel.GetType().FullName + "."
            );
        }
    }
}
