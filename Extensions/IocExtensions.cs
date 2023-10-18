using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Threading;

namespace I18nResourceManager;

/// <summary>
/// IOC拓展
/// </summary>
public static class IocExtensions
{
    #region AddService
    #region Window
    /// <summary>
    /// 添加窗口
    /// </summary>
    /// <typeparam name="TWindow">窗口类型</typeparam>
    /// <typeparam name="TWindowVM">窗口视图模型类型</typeparam>
    /// <param name="services"></param>
    /// <param name="setDataContext">是否在窗口创建时添加数据上下文</param>
    public static void AddWindow<TWindow, TWindowVM>(
        this IServiceCollection services,
        bool setDataContext = true
    )
        where TWindow : Window, new()
        where TWindowVM : ObservableObject, new()
    {
        services.AddTransient<TWindowVM>();
        services.AddTransient<TWindow>(sp =>
        {
            var window = new TWindow();
            if (setDataContext)
                window.DataContext = sp.GetService<TWindowVM>();
            RegisterWindowShutdown(window);
            return window;
        });
    }

    /// <summary>
    /// 添加窗口
    /// </summary>
    /// <typeparam name="TWindow">窗口类型</typeparam>
    /// <typeparam name="TWindowVM">窗口视图模型类型</typeparam>
    /// <param name="services"></param>
    /// <param name="window">窗口</param>
    /// <param name="windowVM">窗口视图模型</param>
    /// <param name="setDataContext">是否在窗口创建时添加数据上下文</param>
    public static void AddWindow<TWindow, TWindowVM>(
        this IServiceCollection services,
        Func<IServiceProvider, TWindow> window,
        Func<IServiceProvider, TWindowVM> windowVM,
        bool setDataContext = true
    )
        where TWindow : Window
        where TWindowVM : ObservableObject
    {
        services.AddTransient<TWindowVM>(sp => windowVM(sp));
        services.AddTransient<TWindow>(sp =>
        {
            var result = window(sp);
            if (setDataContext)
                result.DataContext = sp.GetService<TWindowVM>();
            RegisterWindowShutdown(result);
            return result;
        });
    }
    #endregion

    #region Page
    /// <summary>
    /// 添加页面
    /// </summary>
    /// <typeparam name="TPage">页面类型</typeparam>
    /// <typeparam name="TPageVM">页面视图模型类型</typeparam>
    /// <param name="services"></param>
    /// <param name="setDataContext">是否在页面创建时添加数据上下文</param>
    public static void AddPage<TPage, TPageVM>(
        this IServiceCollection services,
        bool setDataContext = true
    )
        where TPage : Page, new()
        where TPageVM : ObservableObject, new()
    {
        services.AddTransient<TPageVM>();
        services.AddTransient<TPage>(sp =>
        {
            var page = new TPage();
            if (setDataContext)
                page.DataContext = sp.GetService<TPageVM>();
            RegisterDispatcherObjectShutdown(page);
            return page;
        });
    }

    /// <summary>
    /// 添加页面
    /// </summary>
    /// <typeparam name="TPage">页面类型</typeparam>
    /// <typeparam name="TPageVM">页面视图模型类型</typeparam>
    /// <param name="services"></param>
    /// <param name="page">页面</param>
    /// <param name="pageVM">页面视图模型</param>
    /// <param name="setDataContext">是否在窗口创建时添加数据上下文</param>
    public static void AddPage<TPage, TPageVM>(
        this IServiceCollection services,
        Func<IServiceProvider, TPage> page,
        Func<IServiceProvider, TPageVM> pageVM,
        bool setDataContext = true
    )
        where TPage : Page
        where TPageVM : ObservableObject
    {
        services.AddTransient<TPageVM>(sp => pageVM(sp));
        services.AddTransient<TPage>(sp =>
        {
            var result = page(sp);
            if (setDataContext)
                result.DataContext = sp.GetService<TPageVM>();
            RegisterDispatcherObjectShutdown(result);
            return result;
        });
    }
    #endregion

    #region UserControl
    /// <summary>
    /// 添加用户控件
    /// </summary>
    /// <typeparam name="TUserControl">用户控件类型</typeparam>
    /// <typeparam name="TUserControlVM">用户控件视图模型类型</typeparam>
    /// <param name="services"></param>
    /// <param name="setDataContext">是否在用户控件创建时添加数据上下文</param>
    public static void AddUserControl<TUserControl, TUserControlVM>(
        this IServiceCollection services,
        bool setDataContext = true
    )
        where TUserControl : UserControl, new()
        where TUserControlVM : ObservableObject, new()
    {
        services.AddTransient<TUserControlVM>();
        services.AddTransient<TUserControl>(sp =>
        {
            var uc = new TUserControl();
            if (setDataContext)
                uc.DataContext = sp.GetService<TUserControlVM>();
            RegisterDispatcherObjectShutdown(uc);
            return uc;
        });
    }

    /// <summary>
    /// 添加用户控件
    /// </summary>
    /// <typeparam name="TUserControl">用户控件类型</typeparam>
    /// <typeparam name="TUserControlVM">用户控件视图模型类型</typeparam>
    /// <param name="services"></param>
    /// <param name="uc">用户控件</param>
    /// <param name="ucVM">用户控件视图模型</param>
    /// <param name="setDataContext">是否在窗口创建时添加数据上下文</param>
    public static void AddUserControl<TUserControl, TUserControlVM>(
        this IServiceCollection services,
        Func<IServiceProvider, TUserControl> uc,
        Func<IServiceProvider, TUserControlVM> ucVM,
        bool setDataContext = true
    )
        where TUserControl : UserControl
        where TUserControlVM : ObservableObject
    {
        services.AddTransient<TUserControlVM>(sp => ucVM(sp));
        services.AddTransient<TUserControl>(sp =>
        {
            var result = uc(sp);
            if (setDataContext)
                result.DataContext = sp.GetService<TUserControlVM>();
            RegisterDispatcherObjectShutdown(result);
            return result;
        });
    }
    #endregion
    #endregion
    /// <summary>
    /// 注册窗口关闭事件
    /// </summary>
    /// <typeparam name="T">窗口类型</typeparam>
    /// <param name="window">窗口</param>
    public static void RegisterWindowShutdown<T>(T window)
        where T : Window
    {
        window.Closed += Window_Closed;
    }

    /// <summary>
    /// 注册调度关闭事件
    /// </summary>
    /// <typeparam name="T">调度类型</typeparam>
    /// <param name="obj">调度项</param>
    public static void RegisterDispatcherObjectShutdown<T>(T obj)
        where T : DispatcherObject
    {
        obj.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
    }

    /// <summary>
    /// 窗口关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void Window_Closed(object? sender, EventArgs e)
    {
        ClearDataContext(sender);
    }

    /// <summary>
    /// 开始关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void Dispatcher_ShutdownStarted(object? sender, EventArgs e)
    {
        ClearDataContext(sender);
    }

    /// <summary>
    /// 清除DataContext
    /// </summary>
    /// <param name="obj"></param>
    private static void ClearDataContext(object? obj)
    {
        if (obj is FrameworkElement fe)
        {
            try
            {
                fe.DataContext = null;
            }
            catch { }
        }
    }
}
