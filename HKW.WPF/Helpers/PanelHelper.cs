using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class PanelHelper
{
    #region ItemHeight
    /// <summary>
    ///
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static double GetItemHeight(Panel control)
    {
        return (double)control.GetValue(ItemHeightProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetItemHeight(Panel control, double value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWay'."
        );
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.RegisterAttached(
            "ItemHeight",
            typeof(double),
            typeof(PanelHelper),
            new FrameworkPropertyMetadata(default(double), ItemHeightPropertyChanged)
        );

    private static void ItemHeightPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not Panel panel)
            return;
        var height = GetItemHeight(panel);
        for (var i = 0; i < panel.Children.Count; i++)
        {
            var child = panel.Children[i];
            if (child is FrameworkElement element && element.ActualHeight < height)
                element.Height = height;
        }
    }
    #endregion ItemHeight

    #region ItemMargin
    /// <summary>
    ///
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static Thickness GetItemMargin(Panel control)
    {
        return (Thickness)control.GetValue(ItemMarginProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetItemMargin(Panel control, Thickness value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWay'."
        );
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty ItemMarginProperty =
        DependencyProperty.RegisterAttached(
            "ItemMargin",
            typeof(Thickness),
            typeof(PanelHelper),
            new FrameworkPropertyMetadata(default(Thickness), ItemMarginPropertyChanged)
        );

    private static void ItemMarginPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not Panel panel)
            return;
        var margin = GetItemMargin(panel);
        for (var i = 0; i < panel.Children.Count; i++)
        {
            var child = panel.Children[i];
            if (child is FrameworkElement element)
                element.Margin = margin;
        }
    }
    #endregion ItemMargin
}

///// <summary>
/////
///// </summary>
//public static class ItemsControlHelper
//{
//    /// <summary>
//    ///
//    /// </summary>
//    /// <param name="control"></param>
//    /// <returns></returns>
//    public static double GetItemHeight(ItemsControl control)
//    {
//        return (double)control.GetValue(ItemHeightProperty);
//    }

//    /// <summary>
//    ///
//    /// </summary>
//    public static void SetItemHeight(ItemsControl control, double value)
//    {
//        throw new Exception(
//            "This property is read-only. To bind to it you must use 'Mode=OneWay'."
//        );
//    }

//    /// <summary>
//    /// 已选中项目属性
//    /// </summary>
//    public static readonly DependencyProperty ItemHeightProperty =
//        DependencyProperty.RegisterAttached(
//            "ItemHeight",
//            typeof(double),
//            typeof(ItemsControlHelper),
//            new FrameworkPropertyMetadata(default(double), ItemHeightPropertyChanged)
//        );

//    private static void ItemHeightPropertyChanged(
//        DependencyObject obj,
//        DependencyPropertyChangedEventArgs e
//    )
//    {
//        if (obj is not ItemsControl panel)
//            return;
//        var height = GetItemHeight(panel);
//        for (var i = 0; i < panel.Items.Count; i++)
//        {
//            var child = panel.Items[i];
//            if (child is FrameworkElement element)
//                element.Height = height;
//        }
//    }
//}
