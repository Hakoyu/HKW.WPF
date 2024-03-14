using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串大小写转换器
/// </summary>
/// <example>
/// Convert a string to lower case:
/// Text="{Binding Text, Converter={StaticResource StringCaseConverter}, ConverterParameter=L}}"
///
/// Convert a string to upper case:
/// Text="{Binding Text, Converter={StaticResource StringCaseConverter}, ConverterParameter=U}}"
///
/// Convert a string to title case:
/// Text="{Binding Text, Converter={StaticResource StringCaseConverter}, ConverterParameter=T}}"
/// </example>
public class StringCaseConverter : ValueConverterBase<StringCaseConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is string stringValue)
        {
            return parameter?.ToString() switch
            {
                // 大写
                "U" or "u" => culture.TextInfo.ToUpper(stringValue),
                // 小写
                "L" or "l" => culture.TextInfo.ToLower(stringValue),
                // 标题
                "T" or "t" => culture.TextInfo.ToTitleCase(stringValue),
                _
                    => throw new ArgumentException(
                        $"Parameter '{parameter}' is not valid",
                        nameof(parameter)
                    ),
            };
        }

        return null;
    }
}
