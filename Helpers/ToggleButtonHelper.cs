using HKW.WPF.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <param name="toggleButton"></param>
    /// <returns></returns>
    public static bool GetCannotUncheckOnClick(ToggleButton toggleButton)
    {
        return (bool)toggleButton.GetValue(CannotUncheckOnClickProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="toggleButton"></param>
    /// <param name="value"></param>
    public static void SetCannotUncheckOnClick(ToggleButton toggleButton, bool value)
    {
        toggleButton.SetValue(CannotUncheckOnClickProperty, value);
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
        if (obj is not ToggleButton toggleButton)
            return;
        if (e.NewValue is true)
            toggleButton.Click += OnClick;

        static void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton toggleButton)
                return;
            toggleButton.IsChecked = true;
        }
    }
    #endregion

    #region RadioGroup
    /// <summary>
    ///
    /// </summary>
    /// <param name="toggleButton"></param>
    /// <returns></returns>
    public static string GetRadioGroup(ToggleButton toggleButton)
    {
        return (string)toggleButton.GetValue(RadioGroupProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="toggleButton"></param>
    /// <param name="value"></param>
    public static void SetRadioGroup(ToggleButton toggleButton, string value)
    {
        toggleButton.SetValue(RadioGroupProperty, value);
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
        if (obj is not ToggleButton toggleButton)
            return;
        var group = GetRadioGroup(toggleButton);
        FrameworkElement domain;
        if (toggleButton.TryFindParent<Window>(out var window))
            domain = window;
        else if (toggleButton.TryFindParent<Page>(out var page))
            domain = page;
        else
            domain = null!;
        //throw new NotImplementedException();
        _domains.TryAdd(domain, new());
        var domainData = _domains[domain];
        domainData.TryAdd(group, new());
        var groupData = domainData[group];
        groupData.Add(toggleButton);
        toggleButton.Click += OnClick;
        toggleButton.Dispatcher.ShutdownStarted += Button_ShutdownStarted;
        domain.Dispatcher.ShutdownStarted += Domain_ShutdownStarted;

        static void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton toggleButton)
                return;
            var group = GetRadioGroup(toggleButton);
            FrameworkElement domain;
            if (toggleButton.TryFindParent<Window>(out var window))
                domain = window;
            else if (toggleButton.TryFindParent<Page>(out var page))
                domain = page;
            else
                throw new NotImplementedException();
            foreach (var button in _domains[domain][group].Cast<ToggleButton>())
            {
                if (button != toggleButton)
                    button.IsChecked = false;
            }
        }

        static void Button_ShutdownStarted(object? sender, EventArgs e)
        {
            if (sender is not ToggleButton toggleButton)
                return;
            var group = GetRadioGroup(toggleButton);
            FrameworkElement domain;
            if (toggleButton.TryFindParent<Window>(out var window))
                domain = window;
            else if (toggleButton.TryFindParent<Page>(out var page))
                domain = page;
            else
                throw new NotImplementedException();
            var domainData = _domains[domain];
            domainData[group].Remove(toggleButton);
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
