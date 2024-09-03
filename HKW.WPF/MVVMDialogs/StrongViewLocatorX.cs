using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;
using HKW.WPF.MVVMDialogs.Windows;
using Panuon.WPF.UI;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 强视图定位器
/// </summary>
public class StrongViewLocatorX : StrongViewLocator
{
    /// <summary>
    /// 注册页面
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    public void RegisterPage<TViewModel, TPage>()
        where TViewModel : INotifyPropertyChanged
        where TPage : Page, new()
    {
        Register<TViewModel>(new ViewDefinition(typeof(TPage), () => new TPage()));
    }

    /// <summary>
    /// 注册对话框X
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TWindow">窗口类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    public void RegisterDialogX<TViewModel, TWindow, TPage>()
        where TViewModel : INotifyPropertyChanged
        where TPage : Page, new()
        where TWindow : DialogWindowX, new()
    {
        Register<TViewModel>(
            new ViewDefinition(
                typeof(Window),
                () =>
                {
                    var window = new TWindow();
                    var page = new TPage();
                    if (page is IDialogPage dialogPage)
                        dialogPage.DialogWindow = window;
                    window.Frame_Main.Content = page;
                    return window;
                }
            )
        );
    }

    /// <summary>
    /// 注册对话框X
    /// <para>可在创建窗口或页面时进行操作, 例如替换样式</para>
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TWindow">窗口类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    /// <param name="windowAction">窗口行动</param>
    /// <param name="pageAction">页面行动</param>
    public void RegisterDialogX<TViewModel, TWindow, TPage>(
        Action<TWindow>? windowAction,
        Action<TPage>? pageAction
    )
        where TViewModel : INotifyPropertyChanged
        where TPage : Page, new()
        where TWindow : DialogWindowX, new()
    {
        Register<TViewModel>(
            new ViewDefinition(
                typeof(Window),
                () =>
                {
                    var window = new TWindow();
                    var page = new TPage();
                    window.Frame_Main.Content = page;
                    windowAction?.Invoke(window);
                    pageAction?.Invoke(page);
                    return window;
                }
            )
        );
    }
}
