using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到布尔转换器
/// </summary>
public class EnumEqualsConverter : ValueConverterBase<EnumEqualsConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is null)
            return null;
        if (parameter is string str)
        {
            var enumType = value.GetType();
            if (Enum.TryParse(enumType, str, out var parameterValue) is false)
                return null;

            return parameterValue?.Equals(value);
        }

        return null;
    }
}
