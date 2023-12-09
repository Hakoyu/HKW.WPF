using HKW.WPF.Extensions;
using System.Windows.Data;
using System.Windows.Input;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class ElementHelper
{
    #region IsEnabled
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool GetIsEnabled(FrameworkElement element)
    {
        return (bool)element.GetValue(IsEnabledProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用此方法</exception>
    public static void SetIsEnabled(FrameworkElement element, bool value)
    {
        element.SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// 在按下指定按键时清除选中状态
    /// </summary>
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(ElementHelper),
            new FrameworkPropertyMetadata(default(bool), IsEnabledPropertyChangedCallback)
        );

    private static void IsEnabledPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not FrameworkElement element)
            return;
        element.IsEnabled = GetIsEnabled(element);
    }
    #endregion

    #region ClearFocusOnKeyDown
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string GetClearFocusOnKeyDown(FrameworkElement element)
    {
        return (string)element.GetValue(ClearFocusOnKeyDownProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用此方法</exception>
    public static void SetClearFocusOnKeyDown(FrameworkElement element, string value)
    {
        element.SetValue(ClearFocusOnKeyDownProperty, value);
    }

    /// <summary>
    /// 在按下指定按键时清除选中状态
    /// </summary>
    public static readonly DependencyProperty ClearFocusOnKeyDownProperty =
        DependencyProperty.RegisterAttached(
            "ClearFocusOnKeyDown",
            typeof(string),
            typeof(ElementHelper),
            new FrameworkPropertyMetadata(
                default(string),
                ClearFocusOnKeyDownPropertyChangedCallback
            )
        );

    private static void ClearFocusOnKeyDownPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not FrameworkElement element)
            return;
        var keyName = GetClearFocusOnKeyDown(element);
        if (string.IsNullOrWhiteSpace(keyName))
            return;
        if (Enum.TryParse<Key>(keyName, false, out _) is false)
            throw new Exception($"Unknown key name {keyName}");
        element.KeyDown -= Element_KeyDown;
        element.KeyDown += Element_KeyDown;

        static void Element_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            var keyName = GetClearFocusOnKeyDown(element);
            var key = Enum.Parse<Key>(keyName);
            if (e.Key == key)
            {
                // 清除控件焦点
                element.ClearFocus();
                // 清除键盘焦点
                Keyboard.ClearFocus();
            }
        }
    }
    #endregion

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
            typeof(ElementHelper),
            new FrameworkPropertyMetadata(null, UniformMinWidthGroupPropertyChanged)
        );

    /// <summary>
    /// (TopElement ,(GroupName, UniformMinWidthGroupElementInfo))
    /// </summary>
    private readonly static Dictionary<
        FrameworkElement,
        Dictionary<string, UniformMinWidthGroupElementInfo>
    > _uniformMinWidthGroups = new();

    private static void UniformMinWidthGroupPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not FrameworkElement element)
            return;
        var elementTopElement = UniformMinWidthGroupGetTopElement(element);
        if (elementTopElement == null)
            return;
        var currentTopElement = _uniformMinWidthGroups[elementTopElement];
        var groupName = GetUniformMinWidthGroup(element);
        currentTopElement.TryAdd(groupName, new());
        var currentGroup = currentTopElement[groupName];
        currentGroup.Elements.Add(element);
        element.SizeChanged += FrameworkElement_SizeChanged;
        element.Unloaded += FrameworkElement_Unloaded;
        if (elementTopElement is Window window)
            window.Closed += Window_Closed;
        else
            elementTopElement.Unloaded += TopElement_Unloaded;

        static void Window_Closed(object? sender, EventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            _uniformMinWidthGroups.Remove(element);
        }

        static void TopElement_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            _uniformMinWidthGroups.Remove(element);
        }

        static void FrameworkElement_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            var currentTopElement = _uniformMinWidthGroups[
                UniformMinWidthGroupGetTopElement(element)
            ];
            var groupName = GetUniformMinWidthGroup(element);
            currentTopElement[groupName].Elements.Remove(element);
        }

        static void FrameworkElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            var domain = UniformMinWidthGroupGetTopElement(element);
            var currentTopElement = _uniformMinWidthGroups[domain];
            var groupName = GetUniformMinWidthGroup(element);
            var currentGroup = currentTopElement[groupName];
            var maxWidthFE = currentTopElement[groupName].Elements.MaxBy(i => i.ActualWidth);
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
                // 将 MaxWidth 设置为 double.MaxValue 时, 可以让首次加载时进入此处
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
    }

    private static FrameworkElement UniformMinWidthGroupGetTopElement(FrameworkElement element)
    {
        var parent = element.GetTopParent();
        _uniformMinWidthGroups.TryAdd(parent, new());
        return parent;
    }
    #endregion
}

/// <summary>
/// 控件信息
/// </summary>
public class UniformMinWidthGroupElementInfo
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
