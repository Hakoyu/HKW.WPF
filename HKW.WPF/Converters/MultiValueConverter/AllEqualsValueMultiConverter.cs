using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部相等于值转换器
/// </summary>
public class AllEqualsValueMultiConverter<T, TConverter>
    : InvertibleMultiValueConverterBase<TConverter>
    where TConverter : AllEqualsValueMultiConverter<T, TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(T),
        typeof(AllEqualsValueMultiConverter<T, TConverter>)
    );

    /// <summary>
    /// 值
    /// </summary>
    public T Value
    {
        get => (T)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object?[] values,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (IsInverted)
            return values.All(o => o?.Equals(Value) is true);
        else
            return values.All(o => o?.Equals(Value) is not true);
    }
}
