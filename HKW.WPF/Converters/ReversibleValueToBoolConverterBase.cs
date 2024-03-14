using System;
using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 可翻转值到布尔类型转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
/// <typeparam name="TConverter">转换器类型</typeparam>
public abstract class ReversibleValueToBoolConverterBase<T, TConverter>
    : ValueToBoolConverterBase<T, TConverter>
    where TConverter : ValueConverterBase<TConverter>, new()
{
    /// <summary>
    /// 假值
    /// </summary>
    public abstract T FalseValue { get; set; }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty BaseOnFalseValueProperty =
        DependencyProperty.Register(nameof(BaseOnFalseValue), typeof(bool), typeof(T));

    /// <summary>
    /// 以假值为基准
    /// </summary>
    public bool BaseOnFalseValue
    {
        get => (bool)GetValue(BaseOnFalseValueProperty);
        set => SetValue(BaseOnFalseValueProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (!BaseOnFalseValue)
        {
            return base.Convert(value, targetType, parameter, culture);
        }

        var falseValue = FalseValue;
        return !Equals(value, falseValue) ^ IsInverted;
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        return Equals(value) ^ IsInverted ? TrueValue : FalseValue;
    }
}
