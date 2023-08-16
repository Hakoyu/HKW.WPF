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
    /// <summary>
    ///
    /// </summary>
    /// <param name="toggleButton"></param>
    /// <returns></returns>
    public static bool GetCanNotUncheckOnClick(ToggleButton toggleButton)
    {
        return (bool)toggleButton.GetValue(CanNotUncheckOnClickProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="toggleButton"></param>
    /// <param name="value"></param>
    public static void SetCanNotUncheckOnClick(ToggleButton toggleButton, bool value)
    {
        toggleButton.SetValue(CanNotUncheckOnClickProperty, value);
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty CanNotUncheckOnClickProperty =
        DependencyProperty.RegisterAttached(
            "CanNotUncheckOnClick",
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
}
