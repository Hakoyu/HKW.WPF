using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using HKW.WPF.MVVMDialogs;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    #region SetViewModel
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
    #endregion

    #region Show
    /// <summary>
    /// 显示为对话框
    /// </summary>
    /// <param name="window">窗口</param>
    /// <param name="owner">所有者</param>
    /// <param name="windowStartupLocation">窗口显示位置</param>
    /// <returns>对话框结果</returns>
    public static bool? ShowDialog(
        this Window window,
        Window? owner,
        WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterOwner
    )
    {
        window.Owner = owner;
        window.WindowStartupLocation = windowStartupLocation;
        var result = window.ShowDialog();
        // 判断是否为单例对话框, 并收集结果
        if (window is IInstanceDialog dialog)
            result = dialog.InstanceDialogResult;
        return result;
    }

    /// <summary>
    /// 显示或者聚焦
    /// </summary>
    /// <param name="window">窗口</param>
    /// <param name="windowStartupLocation">窗口显示位置</param>
    public static void ShowOrActivate(
        this Window window,
        WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterOwner
    )
    {
        window.WindowStartupLocation = windowStartupLocation;
        if (window.IsVisible is false)
            window.Show();
        window.Activate();
    }

    /// <summary>
    /// 显示或者聚焦
    /// </summary>
    /// <param name="window">窗口</param>
    /// <param name="owner">所有者</param>
    /// <param name="windowStartupLocation">窗口显示位置</param>
    public static void ShowOrActivate(
        this Window window,
        Window? owner,
        WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterOwner
    )
    {
        window.Owner = owner;
        window.WindowStartupLocation = windowStartupLocation;
        if (window.IsVisible is false)
            window.Show();
        window.Activate();
    }

    #endregion

    #region SetLocation
    /// <summary>
    /// 设置位置到中央
    /// <para>(<see langword="Window.IsLoaded"/> 为 <see langword="false"/> 时无效)</para>
    /// </summary>
    /// <param name="window">窗口</param>
    /// <param name="owner">所有者 (如果添加所有这,则会将窗口位置设置到所有者的中心)</param>
    public static void SetLocationToCenter(this Window window, Window? owner = null)
    {
        if (window.IsLoaded is false)
            return;
        var w = SystemParameters.WorkArea.Width;
        var h = SystemParameters.WorkArea.Height;
        if (owner is null)
        {
            window.Left = Math.Clamp((w - window.Width) / 2, 0, w - window.Width);
            window.Top = Math.Clamp((h - window.Height) / 2, 0, h - window.Height);
        }
        else
        {
            var l = owner.Left + owner.Width / 2;
            var t = owner.Top + owner.Height / 2;
            window.Left = Math.Clamp(l - window.Width / 2, 0, w - window.Width);
            window.Top = Math.Clamp(t - window.Height / 2, 0, h - window.Height);
        }
    }
    #endregion
}
