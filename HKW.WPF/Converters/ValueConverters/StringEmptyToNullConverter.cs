using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 空字符串到NULL
/// </summary>
public class StringEmptyToNullConverter : ValueConverterBase<StringEmptyToNullConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is string str && str == string.Empty)
            return null;

        return value;
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is string str && str == string.Empty)
            return null;

        return value;
    }
}
