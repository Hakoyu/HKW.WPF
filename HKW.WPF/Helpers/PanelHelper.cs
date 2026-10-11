using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class PanelHelper
{
    #region ChildHeight
    /// <summary>
    ///
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static double GetChildHeight(Panel control)
    {
        return (double)control.GetValue(ChildHeightProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetChildHeight(Panel control, double value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWay'."
        );
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty ChildHeightProperty =
        DependencyProperty.RegisterAttached(
            "ChildHeight",
            typeof(double),
            typeof(PanelHelper),
            new FrameworkPropertyMetadata(default(double), ChildHeightPropertyChanged)
        );

    private static void ChildHeightPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not Panel panel)
            return;
        var height = GetChildHeight(panel);
        for (var i = 0; i < panel.Children.Count; i++)
        {
            var child = panel.Children[i];
            if (child is FrameworkElement element && element.ActualHeight < height)
                element.Height = height;
        }
    }
    #endregion ChildHeight

    #region ChildMargin
    /// <summary>
    ///
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static Thickness GetChildMargin(Panel control)
    {
        return (Thickness)control.GetValue(ChildMarginProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetChildMargin(Panel control, Thickness value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWay'."
        );
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty ChildMarginProperty =
        DependencyProperty.RegisterAttached(
            "ChildMargin",
            typeof(Thickness),
            typeof(PanelHelper),
            new FrameworkPropertyMetadata(default(Thickness), ChildMarginPropertyChanged)
        );

    private static void ChildMarginPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not Panel panel)
            return;
        var margin = GetChildMargin(panel);
        for (var i = 0; i < panel.Children.Count; i++)
        {
            var child = panel.Children[i];
            if (child is FrameworkElement element)
                element.Margin = margin;
        }
    }
    #endregion ChildMargin


    /// <summary>
    ///
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static int GetShowChildByIndex(Panel control)
    {
        return (int)control.GetValue(ShowChildByIndexProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetShowChildByIndex(Panel control, int value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWay'."
        );
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty ShowChildByIndexProperty =
        DependencyProperty.RegisterAttached(
            "ShowChildByIndex",
            typeof(int),
            typeof(PanelHelper),
            new FrameworkPropertyMetadata(default(int), ShowChildByIndexPropertyChanged)
        );

    private static void ShowChildByIndexPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not Panel panel)
            return;
        var index = GetShowChildByIndex(panel);
        for (var i = 0; i < panel.Children.Count; i++)
        {
            if (panel.Children[i] is not UIElement element)
                continue;
            if (i == index)
            {
                element.Visibility = Visibility.Visible;
            }
            else
            {
                element.Visibility = Visibility.Collapsed;
            }
        }
    }
}
