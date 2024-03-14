using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
/// <typeparam name="TConverter">转换器类型</typeparam>
public class BoolToValueConverter<T, TConverter> : InvertibleValueConverterBase<TConverter>
    where TConverter : BoolToValueConverter<T, TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
        nameof(TrueValue),
        typeof(T),
        typeof(BoolToValueConverter<T, TConverter>)
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
        typeof(BoolToValueConverter<T, TConverter>)
    );

    /// <summary>
    /// 为假时的值
    /// </summary>
    public T FalseValue
    {
        get => (T)GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty NullValueProperty = DependencyProperty.Register(
        nameof(NullValue),
        typeof(bool),
        typeof(BoolToValueConverter<T, TConverter>)
    );

    /// <summary>
    /// 为空时的布尔值
    /// </summary>
    public bool NullValue
    {
        get => (bool)GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        return ConverterUtils.GetBool(value, NullValue) ^ IsInverted ? TrueValue : FalseValue;
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value != null)
            return IsInverted ? value.Equals(FalseValue) : value.Equals(TrueValue);
        else
            return false;
    }
}
