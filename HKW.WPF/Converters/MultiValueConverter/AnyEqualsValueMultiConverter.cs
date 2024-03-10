using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部相等于值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
/// <typeparam name="TConverter">转换器类型</typeparam>
public class AnyEqualsValueMultiConverter<T, TConverter>
    : InvertibleMultiValueConverterBase<TConverter>
    where TConverter : AnyEqualsValueMultiConverter<T, TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(T),
        typeof(AnyEqualsValueMultiConverter<T, TConverter>)
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
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (IsInverted)
            return values.Any(o => o?.Equals(Value) is true);
        else
            return values.Any(o => o?.Equals(Value) is not true);
    }
}
