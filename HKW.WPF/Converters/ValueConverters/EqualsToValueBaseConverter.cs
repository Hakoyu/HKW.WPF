using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等到值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
/// <typeparam name="TConverter">转换器类型</typeparam>
public class EqualsToValueBaseConverter<T, TConverter> : InvertibleValueConverterBase<TConverter>
    where TConverter : EqualsToValueBaseConverter<T, TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
        nameof(TrueValue),
        typeof(T),
        typeof(EqualsToValueBaseConverter<T, TConverter>)
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
        typeof(EqualsToValueBaseConverter<T, TConverter>)
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
        return value?.Equals(parameter) is true ^ IsInverted ? TrueValue : FalseValue;
    }
}
