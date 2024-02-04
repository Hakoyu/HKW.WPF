using System.Windows;
using System.ComponentModel;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部相等于值转换器
/// </summary>
public class AllEqualsValueConverter<T, TConverter> : MultiValueConverterBase<TConverter>
    where TConverter : AllEqualsValueConverter<T, TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(AllEqualsValueConverter<T, TConverter>)
    );

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => (bool)GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(T),
        typeof(AllEqualsValueConverter<T, TConverter>)
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
            return values.All(o => o?.Equals(Value) is true);
        else
            return values.All(o => o?.Equals(Value) is not true);
    }
}
