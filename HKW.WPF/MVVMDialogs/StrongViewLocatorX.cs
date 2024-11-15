using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;
using HKW.MVVMDialogs;
using HKW.WPF.MVVMDialogs.Windows;
using Panuon.WPF.UI;
using ReactiveUI;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 强视图定位器
/// </summary>
public class StrongViewLocatorX : StrongViewLocator
{
    /// <summary>
    /// 定位
    /// </summary>
    protected readonly Dictionary<
        Type,
        (ViewDefinition? Page, ViewDefinition? Window)
    > RegistrationsX = [];

    /// <summary>
    /// 注册窗口
    /// </summary>
    /// <typeparam name="TViewModel">视图模型</typeparam>
    /// <typeparam name="TWindow">窗口</typeparam>
    public new void Register<TViewModel, TWindow>()
        where TViewModel : INotifyPropertyChanged
        where TWindow : Window, new()
    {
        RegistrationsX.Add(typeof(TViewModel), (null!, new(typeof(TWindow), () => new TWindow())));
    }

    /// <summary>
    /// 注册页面
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    public void RegisterPage<TViewModel, TPage>()
        where TViewModel : INotifyPropertyChanged
        where TPage : UserControl, new()
    {
        Register<TViewModel>(new(typeof(TPage), () => new TPage()));
    }

    /// <summary>
    /// 注册页面和窗口
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    /// <typeparam name="TWindow">窗口类型</typeparam>
    public void Register<TViewModel, TPage, TWindow>()
        where TViewModel : INotifyPropertyChanged
        where TPage : UserControl, new()
        where TWindow : Window, new()
    {
        RegistrationsX.Add(
            typeof(TViewModel),
            (new(typeof(TPage), () => new TPage()), new(typeof(TWindow), () => new TWindow()))
        );
    }

    /// <summary>
    /// 注册对话框X
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    public void RegisterDialogX<TViewModel, TPage>()
        where TViewModel : INotifyPropertyChanged
        where TPage : UserControl, new()
    {
        Register<TViewModel>(
            new ViewDefinition(
                typeof(Window),
                () =>
                {
                    var window = new DialogWindowX();
                    var page = new TPage();
                    if (page is IDialogPage<Window> dialogPage)
                        dialogPage.DialogWindow = window;
                    window.ContentControl_Page.Content = page;
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
    /// <typeparam name="TPage">页面类型</typeparam>
    /// <param name="windowAction">窗口行动</param>
    /// <param name="pageAction">页面行动</param>
    public void RegisterDialogX<TViewModel, TPage>(
        Action<TPage>? pageAction,
        Action<DialogWindowX>? windowAction
    )
        where TViewModel : INotifyPropertyChanged
        where TPage : UserControl, new()
    {
        Register<TViewModel>(
            new ViewDefinition(
                typeof(Window),
                () =>
                {
                    var window = new DialogWindowX();
                    var page = new TPage();
                    window.ContentControl_Page.Content = page;
                    windowAction?.Invoke(window);
                    pageAction?.Invoke(page);
                    return window;
                }
            )
        );
    }

    /// <summary>
    /// 注册对话框X
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TWindow">窗口类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    public void RegisterDialogX<TViewModel, TPage, TWindow>()
        where TViewModel : INotifyPropertyChanged
        where TPage : UserControl, new()
        where TWindow : DialogWindowX, new()
    {
        Register<TViewModel>(
            new ViewDefinition(
                typeof(Window),
                () =>
                {
                    var window = new TWindow();
                    var page = new TPage();
                    if (page is IDialogPage<Window> dialogPage)
                        dialogPage.DialogWindow = window;
                    window.ContentControl_Page.Content = page;
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
    public void RegisterDialogX<TViewModel, TPage, TWindow>(
        Action<TPage>? pageAction,
        Action<TWindow>? windowAction
    )
        where TViewModel : INotifyPropertyChanged
        where TPage : UserControl, new()
        where TWindow : DialogWindowX, new()
    {
        Register<TViewModel>(
            new ViewDefinition(
                typeof(Window),
                () =>
                {
                    var window = new TWindow();
                    var page = new TPage();
                    window.ContentControl_Page.Content = page;
                    windowAction?.Invoke(window);
                    pageAction?.Invoke(page);
                    return window;
                }
            )
        );
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <typeparam name="TViewModel">视图模型</typeparam>
    /// <param name="viewDef">视图定义</param>
    protected new void Register<TViewModel>(ViewDefinition viewDef)
        where TViewModel : INotifyPropertyChanged
    {
        RegistrationsX.Add(typeof(TViewModel), (null!, viewDef));
    }

    /// <inheritdoc/>
    public ViewDefinition Locate<TViewModel>()
        where TViewModel : INotifyPropertyChanged
    {
        var type = typeof(TViewModel);
        if (RegistrationsX.TryGetValue(type, out var value))
        {
            return value.Window!.Value;
        }

        throw new TypeLoadException(
            string.Concat(
                "No view was registered for view model " + type.FullName + ".",
                Environment.NewLine,
                "This project uses a StrongViewLocator, which requires manually registering all ViewModel-View combinations."
            )
        );
    }

    /// <inheritdoc/>
    public override ViewDefinition Locate(object viewModel)
    {
        var type = viewModel.GetType();
        if (RegistrationsX.TryGetValue(type, out var value))
        {
            return value.Window!.Value;
        }

        throw new TypeLoadException(
            string.Concat(
                "No view was registered for view model " + type.FullName + ".",
                Environment.NewLine,
                "This project uses a StrongViewLocator, which requires manually registering all ViewModel-View combinations."
            )
        );
    }

    /// <summary>
    /// 定位
    /// </summary>
    /// <param name="viewModel">视图模型</param>
    /// <returns>(PageViewDefinition, WindowViewDefinition)</returns>
    public (ViewDefinition? Page, ViewDefinition? Window) LocateX(object viewModel)
    {
        if (RegistrationsX.TryGetValue(viewModel.GetType(), out var value))
        {
            return value;
        }

        throw new TypeLoadException(
            string.Concat(
                "No view was registered for view model " + viewModel.GetType().FullName + ".",
                Environment.NewLine,
                "This project uses a StrongViewLocator, which requires manually registering all ViewModel-View combinations."
            )
        );
    }

    /// <summary>
    /// 创建页面
    /// </summary>
    /// <param name="viewModel">视图模型</param>
    /// <returns>页面</returns>
    public object CreatePage(object viewModel)
    {
        return LocateX(viewModel).Page?.Create()!;
    }
}
