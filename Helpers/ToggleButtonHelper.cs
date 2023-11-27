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
    /// 无法在 <see cref="ToggleButton.OnClick"/> 中取消 <see cref="ToggleButton.IsChecked"/> 状态,只能手动设置 <see cref="ToggleButton.IsChecked"/>
    /// </summary>
    public static readonly DependencyProperty CannotUncheckOnClickProperty =
        DependencyProperty.RegisterAttached(
            "CannotUncheckOnClick",
            typeof(bool),
            typeof(ToggleButtonHelper),
            new FrameworkPropertyMetadata(
                default(bool),
                CanNotUncheckOnClickPropertyChangedCallback
            )
        );

    private static void CanNotUncheckOnClickPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ToggleButton element)
            return;
        if (e.NewValue is true)
            element.Click += OnClick;

        static void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton element)
                return;
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
    /// ToggleButton 单选分组m
    /// </summary>
    public static readonly DependencyProperty RadioGroupProperty =
        DependencyProperty.RegisterAttached(
            "RadioGroup",
            typeof(string),
            typeof(ToggleButtonHelper),
            new FrameworkPropertyMetadata(default(string), RadioGroupPropertyChanged)
        );

    private static readonly Dictionary<
        FrameworkElement,
        Dictionary<string, HashSet<FrameworkElement>>
    > _domains = new();

    private static void RadioGroupPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ToggleButton element)
            return;
        var group = GetRadioGroup(element);
        FrameworkElement domain;
        if (element.TryFindParent<Window>(out var window))
            domain = window;
        else if (element.TryFindParent<Page>(out var page))
            domain = page;
        else
            domain = null!;
        //throw new NotImplementedException();
        _domains.TryAdd(domain, new());
        var domainData = _domains[domain];
        domainData.TryAdd(group, new());
        var groupData = domainData[group];
        groupData.Add(element);
        element.Click += OnClick;
        element.Dispatcher.ShutdownStarted += Button_ShutdownStarted;
        domain.Dispatcher.ShutdownStarted += Domain_ShutdownStarted;

        static void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton element)
                return;
            var group = GetRadioGroup(element);
            FrameworkElement domain;
            if (element.TryFindParent<Window>(out var window))
                domain = window;
            else if (element.TryFindParent<Page>(out var page))
                domain = page;
            else
                throw new NotImplementedException();
            foreach (var button in _domains[domain][group].Cast<ToggleButton>())
            {
                if (button != element)
                    button.IsChecked = false;
            }
        }

        static void Button_ShutdownStarted(object? sender, EventArgs e)
        {
            if (sender is not ToggleButton element)
                return;
            var group = GetRadioGroup(element);
            FrameworkElement domain;
            if (element.TryFindParent<Window>(out var window))
                domain = window;
            else if (element.TryFindParent<Page>(out var page))
                domain = page;
            else
                throw new NotImplementedException();
            var domainData = _domains[domain];
            domainData[group].Remove(element);
        }
        static void Domain_ShutdownStarted(object? sender, EventArgs e)
        {
            if (sender is not FrameworkElement fe)
                return;
            _domains.Remove(fe);
        }
    }

    #endregion
}
