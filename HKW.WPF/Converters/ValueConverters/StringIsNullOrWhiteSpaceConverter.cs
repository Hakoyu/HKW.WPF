using System.Windows;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串是null或者空白转换器
/// </summary>
public class StringIsNullOrWhiteSpaceConverter
    : ValueConverterBase<StringIsNullOrWhiteSpaceConverter>
{
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(StringIsNullOrWhiteSpaceConverter)
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
        return string.IsNullOrWhiteSpace(value as string) ^ IsInverted;
    }
}
