using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Helpers;

/// <summary>
/// 堆栈面板助手
/// </summary>
public static class StackPanelHelper
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Thickness GetMargen(StackPanel obj) =>
        obj is not null ? (Thickness)obj.GetValue(MargenProperty) : new Thickness();

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetMargen(StackPanel obj, Thickness value) =>
        obj?.SetValue(MargenProperty, value);

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty MargenProperty = DependencyProperty.RegisterAttached(
        "Margen",
        typeof(Thickness),
        typeof(StackPanelHelper),
        new UIPropertyMetadata(new Thickness(), MargenPropertyChangedCallback)
    );

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool GetIgnoreTopMargen(StackPanel obj) =>
        obj is not null && (bool)obj.GetValue(IgnoreTopMargenProperty);

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetIgnoreTopMargen(StackPanel obj, bool value) =>
        obj.SetValue(IgnoreTopMargenProperty, value);

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IgnoreTopMargenProperty =
        DependencyProperty.RegisterAttached(
            "IgnoreTopMargen",
            typeof(bool),
            typeof(StackPanelHelper)
        );

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool GetIgnoreLeftMargen(StackPanel obj) =>
        obj is not null && (bool)obj.GetValue(IgnoreLeftMargenProperty);

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetIgnoreLeftMargen(StackPanel obj, bool value) =>
        obj.SetValue(IgnoreLeftMargenProperty, value);

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IgnoreLeftMargenProperty =
        DependencyProperty.RegisterAttached(
            "IgnoreLeftMargen",
            typeof(bool),
            typeof(StackPanelHelper)
        );

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool GetIgnoreRightMargen(StackPanel obj) =>
        obj is not null && (bool)obj.GetValue(IgnoreRightMargenProperty);

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetIgnoreRightMargen(StackPanel obj, bool value) =>
        obj.SetValue(IgnoreRightMargenProperty, value);

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IgnoreRightMargenProperty =
        DependencyProperty.RegisterAttached(
            "IgnoreRightMargen",
            typeof(bool),
            typeof(StackPanelHelper)
        );

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool GetIgnoreButtomMargen(StackPanel obj) =>
        obj is not null && (bool)obj.GetValue(IgnoreButtomMargenProperty);

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetIgnoreButtomMargen(StackPanel obj, bool value) =>
        obj.SetValue(IgnoreButtomMargenProperty, value);

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IgnoreButtomMargenProperty =
        DependencyProperty.RegisterAttached(
            "IgnoreButtomMargen",
            typeof(bool),
            typeof(StackPanelHelper)
        );

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public static void MargenPropertyChangedCallback(
        object obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not Panel panel)
            return;
        panel.Loaded += SetMarginControlsChildren;

        static void SetMarginControlsChildren(object obj, RoutedEventArgs e)
        {
            if (obj is not StackPanel panel)
                return;
            FrameworkElement? firstFE = null;
            FrameworkElement? lastFE = null;
            foreach (var child in panel.Children)
            {
                if (child is not FrameworkElement fe)
                    continue;
                firstFE ??= fe;
                lastFE = fe;
                fe.Margin = GetMargen(panel);
                if (GetIgnoreLeftMargen(panel))
                    fe.Margin = new Thickness(0, fe.Margin.Top, fe.Margin.Right, fe.Margin.Bottom);
                if (GetIgnoreRightMargen(panel))
                    fe.Margin = new Thickness(fe.Margin.Left, fe.Margin.Top, 0, fe.Margin.Bottom);
            }
            if (firstFE is not null && GetIgnoreTopMargen(panel))
                firstFE.Margin = new Thickness(
                    GetIgnoreLeftMargen(panel) ? 0 : firstFE.Margin.Left,
                    0,
                    GetIgnoreRightMargen(panel) ? 0 : firstFE.Margin.Right,
                    firstFE.Margin.Bottom
                );
            if (lastFE is not null && GetIgnoreButtomMargen(panel))
                lastFE.Margin = new Thickness(
                    GetIgnoreLeftMargen(panel) ? 0 : lastFE.Margin.Left,
                    lastFE.Margin.Top,
                    GetIgnoreRightMargen(panel) ? 0 : lastFE.Margin.Right,
                    0
                );
        }
    }
}
