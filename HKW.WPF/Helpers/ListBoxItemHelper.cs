using HKW.WPF.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.Helpers;

/// <summary>
/// 列表框项目助手
/// </summary>
public static class ListBoxItemHelper
{
    #region ScrollToControlTargetScrollViewer
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static ScrollViewer GetScrollToControlTargetScrollViewer(ListBoxItem element)
    {
        return (ScrollViewer)element.GetValue(ScrollToControlTargetScrollViewerProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetScrollToControlTargetScrollViewer(ListBoxItem element, ScrollViewer value)
    {
        element.SetValue(ScrollToControlTargetScrollViewerProperty, value);
    }

    /// <summary>
    /// 在按下指定按键时清除选中状态
    /// </summary>
    public static readonly DependencyProperty ScrollToControlTargetScrollViewerProperty =
        DependencyProperty.RegisterAttached(
            "ScrollToControlTargetScrollViewer",
            typeof(ScrollViewer),
            typeof(ListBoxItemHelper),
            new FrameworkPropertyMetadata(null, ScrollToControlTargetScrollViewerChanged)
        );

    private static void ScrollToControlTargetScrollViewerChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ListBoxItem listBoxItem)
            return;
        listBoxItem.Selected += ListBoxItem_Selected;
    }

    private static void ListBoxItem_Selected(object sender, RoutedEventArgs e)
    {
        if (sender is not ListBoxItem listBoxItem)
            return;
        var targetScrollViewer = GetScrollToControlTargetScrollViewer(listBoxItem);
        var targetControl = GetScrollToControlTargetControl(listBoxItem);
        if (targetControl is null || targetScrollViewer is null)
            throw new ArgumentNullException(
                $"{targetScrollViewer} or {targetControl} cannot be null"
            );
        // 检查指定的控件是否在指定的 ScrollViewer 中
        var result = targetControl.FindVisuaParent<ScrollViewer>();
        if (result is null || result != targetScrollViewer)
            throw new Exception("The target Control is not in the target ScrollViewer");

        // 获取要定位之前 ScrollViewer 目前的滚动位置
        var currentScrollPosition = targetScrollViewer.VerticalOffset;
        var point = new Point(0, currentScrollPosition);

        // 计算出目标位置并滚动
        var targetPosition = targetControl.TransformToVisual(targetScrollViewer).Transform(point);
        targetScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
    }
    #endregion

    #region ScrollToControlTargetControl
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static FrameworkElement GetScrollToControlTargetControl(ListBoxItem element)
    {
        return (FrameworkElement)element.GetValue(ScrollToControlTargetControlProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetScrollToControlTargetControl(ListBoxItem element, FrameworkElement value)
    {
        element.SetValue(ScrollToControlTargetControlProperty, value);
    }

    /// <summary>
    /// 在按下指定按键时清除选中状态
    /// </summary>
    public static readonly DependencyProperty ScrollToControlTargetControlProperty =
        DependencyProperty.RegisterAttached(
            "ScrollToControlTargetControl",
            typeof(FrameworkElement),
            typeof(ListBoxItemHelper),
            new FrameworkPropertyMetadata(null)
        );
    #endregion
}
