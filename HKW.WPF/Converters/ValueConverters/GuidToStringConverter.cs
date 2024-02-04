using System.Windows;
using System;

namespace HKW.WPF.Converters;

/// <summary>
/// Guid到字符串转换器
/// </summary>
public class GuidToStringConverter : ValueConverterBase<GuidToStringConverter>
{
    /// <summary>
    /// 默认格式化
    /// </summary>
    protected const string DefaultFormat = "D";

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty ToUpperProperty = DependencyProperty.Register(
        nameof(ToUpper),
        typeof(bool),
        typeof(GuidToStringConverter)
    );

    /// <summary>
    /// 转换为大写
    /// </summary>
    public bool ToUpper
    {
        get => (bool)GetValue(ToUpperProperty);
        set => SetValue(ToUpperProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
        nameof(Format),
        typeof(string),
        typeof(GuidToStringConverter),
        new(DefaultFormat)
    );

    /// <summary>
    /// 格式化
    /// </summary>
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is Guid guid)
        {
            var guidString = guid.ToString(Format);

            if (ToUpper)
                return guidString.ToUpperInvariant();

            return guidString;
        }

        return null;
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is string guidString)
        {
            var guid = new Guid(guidString);
            return guid;
        }

        return null;
    }
}
