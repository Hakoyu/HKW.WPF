using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 设置视图模型
    /// </summary>
    /// <typeparam name="T">视图模型类型</typeparam>
    /// <param name="window">窗口</param>
    /// <param name="closedEvent">关闭事件</param>
    /// <returns>视图模型</returns>
    public static T SetViewModel<T>(this Window window, EventHandler? closedEvent = null)
        where T : INotifyPropertyChanged, new()
    {
        if (window.DataContext is null)
        {
            window.DataContext = new T();
            window.Closed += (s, e) =>
            {
                try
                {
                    window.DataContext = null;
                }
                catch { }
            };
            window.Closed += closedEvent;
        }
        return (T)window.DataContext;
    }

    /// <summary>
    /// 设置视图模型
    /// </summary>
    /// <typeparam name="T">视图模型类型</typeparam>
    /// <param name="window">窗口</param>
    /// <param name="viewModel">视图模型</param>
    /// <param name="closedEvent">关闭事件</param>
    /// <returns>视图模型</returns>
    public static T SetViewModel<T>(
        this Window window,
        T viewModel,
        EventHandler? closedEvent = null
    )
        where T : INotifyPropertyChanged
    {
        if (window.DataContext is null)
        {
            window.DataContext = viewModel;
            window.Closed += (s, e) =>
            {
                try
                {
                    window.DataContext = null;
                }
                catch { }
            };
            window.Closed += closedEvent;
        }
        return viewModel;
    }

    /// <summary>
    /// 设置视图模型
    /// </summary>
    /// <typeparam name="T">视图模型类型</typeparam>
    /// <param name="page">页面</param>
    public static T SetViewModel<T>(this Page page)
        where T : INotifyPropertyChanged, new()
    {
        return (T)(page.DataContext ??= new T());
    }

    /// <summary>
    /// 显示为对话框
    /// </summary>
    /// <param name="window">窗口</param>
    /// <param name="owner">所有者</param>
    /// <returns>对话框结果</returns>
    public static bool? ShowDialog(this Window window, Window owner)
    {
        window.Owner = owner;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        return window.ShowDialog();
    }
}
