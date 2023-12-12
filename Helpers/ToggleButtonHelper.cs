using HKW.WPF.Extensions;
using System.Windows.Controls.Primitives;

namespace HKW.WPF.Helpers;

/// <summary>
/// 可切换按钮助手
/// </summary>
public static class ToggleButtonHelper
{
    #region CanNotUncheckOnClick
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool GetCannotUncheckOnClick(ToggleButton element)
    {
        return (bool)element.GetValue(CannotUncheckOnClickProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetCannotUncheckOnClick(ToggleButton element, bool value)
    {
        element.SetValue(CannotUncheckOnClickProperty, value);
    }

    /// <summary>
    /// 无法在 <see cref="ToggleButton.OnClick"/> 中取消 <see cref="ToggleButton.IsChecked"/> 状态, 只能手动设置 <see cref="ToggleButton.IsChecked"/>
    /// </summary>
    public static readonly DependencyProperty CannotUncheckOnClickProperty =
        DependencyProperty.RegisterAttached(
            "CannotUncheckOnClick",
            typeof(bool),
            typeof(ToggleButtonHelper),
            new FrameworkPropertyMetadata(default(bool), CanNotUncheckOnClickPropertyChanged)
        );

    private static void CanNotUncheckOnClickPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ToggleButton element)
            return;
        element.Click -= Element_Click;
        element.Click += Element_Click;

        static void Element_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton element)
                return;
            if (element.IsChecked is not true)
                element.IsChecked = true;
        }
    }
    #endregion

    #region RadioGroup
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string GetRadioGroup(ToggleButton element)
    {
        return (string)element.GetValue(RadioGroupProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetRadioGroup(ToggleButton element, string value)
    {
        element.SetValue(RadioGroupProperty, value);
    }

    /// <summary>
    /// ToggleButton 单选分组
    /// </summary>
    public static readonly DependencyProperty RadioGroupProperty =
        DependencyProperty.RegisterAttached(
            "RadioGroup",
            typeof(string),
            typeof(ToggleButtonHelper),
            new FrameworkPropertyMetadata(default(string), RadioGroupPropertyChanged)
        );

    /// <summary>
    /// (TopElement, (GroupName, Elements))
    /// </summary>
    private static readonly Dictionary<
        FrameworkElement,
        Dictionary<string, HashSet<FrameworkElement>>
    > _radioGroupDatas = new();

    private static void RadioGroupPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ToggleButton element)
            return;
        var group = GetRadioGroup(element);
        var topParent = element.FindTopParent();
        _radioGroupDatas.TryAdd(topParent, new());
        var topParentData = _radioGroupDatas[topParent];
        topParentData.TryAdd(group, new());
        var groupData = topParentData[group];
        groupData.Add(element);
        element.Click += Element_Click;
        element.Unloaded += Element_Unloaded;
        topParent.Unloaded += TopParent_Unloaded;

        static void Element_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton element)
                return;
            var group = GetRadioGroup(element);
            var topParent = element.FindTopParent();
            foreach (var button in _radioGroupDatas[topParent][group].Cast<ToggleButton>())
            {
                if (button != element)
                    button.IsChecked = false;
            }
        }

        static void Element_Unloaded(object? sender, EventArgs e)
        {
            if (sender is not ToggleButton element)
                return;
            var group = GetRadioGroup(element);
            var topParent = element.FindTopParent();
            var topParentData = _radioGroupDatas[topParent];
            topParentData[group].Remove(element);
        }

        static void TopParent_Unloaded(object? sender, EventArgs e)
        {
            if (sender is not FrameworkElement fe)
                return;
            _radioGroupDatas.Remove(fe);
        }
    }

    #endregion
}
