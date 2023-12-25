using HKW.WPF.Extensions;

namespace HKW.WPF.Behaviors;

/// <summary>
/// 在 ScrollViewer 中定位到指定的控件
/// </summary>
public class ScrollToControlAction : TriggerAction<FrameworkElement>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register(
        "ScrollViewer",
        typeof(ScrollViewer),
        typeof(ScrollToControlAction)
    );

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TargetControlProperty = DependencyProperty.Register(
        "TargetControl",
        typeof(FrameworkElement),
        typeof(ScrollToControlAction)
    );

    /// <summary>
    /// 目标 ScrollViewer
    /// </summary>
    public ScrollViewer ScrollViewer
    {
        get => (ScrollViewer)GetValue(ScrollViewerProperty);
        set => SetValue(ScrollViewerProperty, value);
    }

    /// <summary>
    /// 要定位的到的控件
    /// </summary>
    public FrameworkElement TargetControl
    {
        get => (FrameworkElement)GetValue(TargetControlProperty);
        set => SetValue(TargetControlProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="parameter"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    protected override void Invoke(object parameter)
    {
        if (TargetControl is null || ScrollViewer is null)
            throw new ArgumentNullException($"{ScrollViewer} or {TargetControl} cannot be null");
        // 检查指定的控件是否在指定的 ScrollViewer 中
        var result = TargetControl.FindVisuaParent<ScrollViewer>();
        if (result is null || result != ScrollViewer)
            throw new Exception("The target Control is not in the target ScrollViewer");

        // 获取要定位之前 ScrollViewer 目前的滚动位置
        var currentScrollPosition = ScrollViewer.VerticalOffset;
        var point = new Point(0, currentScrollPosition);

        // 计算出目标位置并滚动
        var targetPosition = TargetControl.TransformToVisual(ScrollViewer).Transform(point);
        ScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
    }
}
