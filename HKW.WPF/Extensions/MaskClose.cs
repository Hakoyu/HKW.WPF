using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    #region MaskClose
    /// <summary>
    /// 屏蔽关闭事件的窗口
    /// </summary>
    private static readonly HashSet<Window> _maskClosedWindows = [];

    /// <summary>
    /// 效果下一次关闭事件的窗口
    /// </summary>
    private static readonly HashSet<Window> _skipNextWindows = [];

    /// <summary>
    /// 是屏蔽关闭事件
    /// </summary>
    /// <param name="window">窗口</param>
    /// <returns>是为 <see langword="true"/> 不是为 <see langword="false"/></returns>
    public static bool IsMaskClose(this Window window)
    {
        return _maskClosedWindows.Contains(window);
    }

    /// <summary>
    /// 屏蔽关闭事件
    /// </summary>
    /// <param name="window">窗口</param>
    /// <param name="owner">所有者 (当所有者关闭时, 此窗口同时关闭)</param>
    public static TWindow MaskClose<TWindow>(this TWindow window, Window? owner = null)
        where TWindow : Window
    {
        window.Closing -= Window_MaskClose_Closing;
        window.Closed -= Window_MaskClose_Closed;

        window.Closing += Window_MaskClose_Closing;
        window.Closed += Window_MaskClose_Closed;

        _maskClosedWindows.Add(window);
        if (owner is not null)
        {
            owner.Closed += (s, e) =>
            {
                window.CloseX();
            };
        }
        return window;
    }

    private static void Window_MaskClose_Closed(object? sender, EventArgs e)
    {
        if (sender is not Window window)
            return;
        _maskClosedWindows.Remove(window);
    }

    private static void Window_MaskClose_Closing(object? sender, CancelEventArgs e)
    {
        if (sender is not Window window)
            return;
        if (_maskClosedWindows.Contains(window))
        {
            e.Cancel = true;
            if (_skipNextWindows.Contains(window))
            {
                _skipNextWindows.Remove(window);
                return;
            }
            //window.Visibility = Visibility.Collapsed;
            window.Hide();
        }
    }

    /// <summary>
    /// 强制关闭
    /// </summary>
    /// <param name="window">窗口</param>
    public static void CloseX(this Window window)
    {
        _maskClosedWindows.Remove(window);
        window.Closing -= Window_MaskClose_Closing;
        window.Closed -= Window_MaskClose_Closed;
        window.Close();
    }

    /// <summary>
    /// 跳过下一次关闭
    /// </summary>
    /// <param name="window">窗口</param>
    public static void SkipNextClose(this Window window)
    {
        _skipNextWindows.Add(window);
    }
    #endregion
}
