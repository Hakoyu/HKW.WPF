﻿using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等到值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
/// <typeparam name="TConverter">转换器类型</typeparam>
public class EqualsStringToValueBaseConverter<T, TConverter>
    : InvertibleValueConverterBase<TConverter>
    where TConverter : EqualsStringToValueBaseConverter<T, TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
        nameof(TrueValue),
        typeof(T),
        typeof(EqualsStringToValueBaseConverter<T, TConverter>)
    );

    /// <summary>
    /// 为真时的值
    /// </summary>
    public T TrueValue
    {
        get => (T)GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty FalseValueProperty = DependencyProperty.Register(
        nameof(FalseValue),
        typeof(T),
        typeof(EqualsStringToValueBaseConverter<T, TConverter>)
    );

    /// <summary>
    /// 为假时的值
    /// </summary>
    public T FalseValue
    {
        get => (T)GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is null || parameter is null)
            return IsInverted;
        return value.ToString() == parameter.ToString() ^ IsInverted ? TrueValue : FalseValue;
    }
}