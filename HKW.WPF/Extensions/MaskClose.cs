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
    private static readonly HashSet<Window> _maskClosedWindow = [];

    /// <summary>
    /// 是屏蔽关闭事件
    /// </summary>
    /// <param name="window">窗口</param>
    /// <returns>是为 <see langword="true"/> 不是为 <see langword="false"/></returns>
    public static bool IsMaskClose(this Window window)
    {
        return _maskClosedWindow.Contains(window);
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

        _maskClosedWindow.Add(window);
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
        _maskClosedWindow.Remove(window);
    }

    private static void Window_MaskClose_Closing(object? sender, CancelEventArgs e)
    {
        if (sender is not Window window)
            return;
        if (_maskClosedWindow.Contains(window))
        {
            e.Cancel = true;
            //window.Visibility = Visibility.Collapsed;
            window.Hide();
        }
    }

    /// <summary>
    /// 强制关闭
    /// </summary>
    /// <param name="window"></param>
    public static void CloseX(this Window window)
    {
        _maskClosedWindow.Remove(window);
        window.Closing -= Window_MaskClose_Closing;
        window.Closed -= Window_MaskClose_Closed;
        window.Close();
    }
    #endregion
}
