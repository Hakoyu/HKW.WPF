using System;
using System.Collections;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 第一个或默认转换器
/// </summary>
public class FirstOrDefaultConverter : ValueConverterBase<FirstOrDefaultConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            {
                if (enumerable.GetEnumerator().MoveNext())
                {
                    return enumerator.Current;
                }
            }
        }

        return null;
    }
}
