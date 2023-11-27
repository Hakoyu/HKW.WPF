using HKW.WPF.Extensions;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class FrameworkElementHelper
{
    #region UniformMinWidthGroup
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string GetUniformMinWidthGroup(FrameworkElement element)
    {
        return (string)element.GetValue(UniformWidthGroupProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetUniformMinWidthGroup(FrameworkElement element, string value)
    {
        element.SetValue(UniformWidthGroupProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty UniformWidthGroupProperty =
        DependencyProperty.RegisterAttached(
            "UniformMinWidthGroup",
            typeof(string),
            typeof(FrameworkElementHelper),
            new FrameworkPropertyMetadata(null, UniformMinWidthGroupPropertyChanged)
        );

    /// <summary>
    /// (Domain ,(GroupName, FEInfos))
    /// </summary>
    private readonly static Dictionary<
        FrameworkElement,
        Dictionary<string, ElementInfo>
    > _uniformMinWidthGroupDomains = new();

    private static void UniformMinWidthGroupPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not FrameworkElement element)
            return;
        var elementDomain = GetDomain(element);
        if (elementDomain == null)
            return;
        var currentDomain = _uniformMinWidthGroupDomains[elementDomain];
        var groupName = GetUniformMinWidthGroup(element);
        currentDomain.TryAdd(groupName, new());
        var currentGroup = currentDomain[groupName];
        element.Tag = false;
        currentGroup.Elements.Add(element);
        element.SizeChanged += UniformMinWidthGroup_FE_SizeChanged;
        element.Unloaded += UniformMinWidthGroup_FE_Unloaded;
        if (elementDomain is Window window)
            window.Closed += UniformMinWidthGroup_Window_Closed;
        else
            elementDomain.Unloaded += UniformMinWidthGroup_Domain_Unloaded;
    }

    private static void UniformMinWidthGroup_Window_Closed(object? sender, EventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;
        _uniformMinWidthGroupDomains.Remove(element);
    }

    private static void UniformMinWidthGroup_Domain_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;
        _uniformMinWidthGroupDomains.Remove(element);
    }

    private static void UniformMinWidthGroup_FE_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;
        var currentDomain = _uniformMinWidthGroupDomains[GetDomain(element)];
        var groupName = GetUniformMinWidthGroup(element);
        currentDomain[groupName].Elements.Remove(element);
    }

    private static void UniformMinWidthGroup_FE_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;
        var domain = GetDomain(element);
        var currentDomain = _uniformMinWidthGroupDomains[domain];
        var groupName = GetUniformMinWidthGroup(element);
        var currentGroup = currentDomain[groupName];
        var maxWidthFE = currentDomain[groupName].Elements.MaxBy(i => i.ActualWidth);
        if (maxWidthFE is null)
            return;
        if (maxWidthFE.ActualWidth == element.ActualWidth)
            maxWidthFE = element;
        if (maxWidthFE.ActualWidth > currentGroup.MaxWidth)
        {
            // 如果当前控件最大宽度的超过历史最大宽度, 表面非最大宽度列表中的控件超过了历史最大宽度
            foreach (var item in currentGroup.Elements)
                item.MinWidth = maxWidthFE.ActualWidth;
            // 将当前控件最小宽度设为0
            maxWidthFE.MinWidth = 0;
            currentGroup.MaxWidthElements.Clear();
            // 设为最大宽度的唯一控件
            currentGroup.MaxWidthElements.Add(maxWidthFE);
            currentGroup.MaxWidth = maxWidthFE.ActualWidth;
        }
        else if (currentGroup.MaxWidthElements.Count == 1)
        {
            var current = currentGroup.MaxWidthElements.First();
            // 当最大宽度控件只有一个时, 并且当前控件宽度小于历史最大宽度时, 表明需要降低全体宽度
            if (currentGroup.MaxWidth > current.ActualWidth)
            {
                // 最小宽度设为0以自适应宽度
                foreach (var item in currentGroup.Elements)
                    item.MinWidth = 0;
                // 清空最大宽度列表, 让其刷新
                currentGroup.MaxWidthElements.Clear();
            }
        }
        else
        {
            // 将 FEInfos.LastMax 设置为 double.MaxValue 时, 可以让首次加载时进入此处
            foreach (var item in currentGroup.Elements)
            {
                // 当控件最小宽度为0(表示其为主导宽度的控件), 并且其宽度等于最大宽度, 加入最大宽度列表
                if (item.MinWidth == 0 && item.ActualWidth == maxWidthFE.ActualWidth)
                {
                    currentGroup.MaxWidthElements.Add(item);
                }
                else
                {
                    // 如果不是, 则从最大宽度列表删除, 并设置最小宽度为当前最大宽度
                    currentGroup.MaxWidthElements.Remove(item);
                    item.MinWidth = maxWidthFE.ActualWidth;
                }
            }
            currentGroup.MaxWidth = maxWidthFE.ActualWidth;
        }
    }

    private static FrameworkElement GetDomain(FrameworkElement element)
    {
        FrameworkElement elementParent;
        if (element.FindParent<Window>() is Window window)
        {
            elementParent = window;
            _uniformMinWidthGroupDomains.TryAdd(window, new());
        }
        else if (element.FindParent<Page>() is Page page)
        {
            elementParent = page;
            _uniformMinWidthGroupDomains.TryAdd(page, new());
        }
        else
        {
            return null!;
        }
        return elementParent;
    }
    #endregion
}

/// <summary>
/// 控件信息
/// </summary>
public class ElementInfo
{
    /// <summary>
    /// 所有控件
    /// </summary>
    public HashSet<FrameworkElement> Elements { get; } = new();

    /// <summary>
    /// 最后一个最大宽度
    /// </summary>
    public double MaxWidth { get; set; } = double.MaxValue;

    /// <summary>
    /// 最大宽度的控件
    /// </summary>
    public HashSet<FrameworkElement> MaxWidthElements { get; } = new();
}
