using System.Globalization;
using System;
using System.Collections;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 空到布尔转换器
/// </summary>
public class NullToBoolConverter : ValueConverterBase<NullToBoolConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(NullToBoolConverter)
    );

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => (bool)GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return value == null ^ IsInverted;
    }
}
