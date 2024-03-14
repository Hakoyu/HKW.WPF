using System;
using System.Collections;
using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 集合为空转换器
/// </summary>
public class CollectionIsEmptyConverter : InvertibleValueConverterBase<CollectionIsEmptyConverter>
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
            return (enumerable.GetEnumerator().MoveNext() is false) ^ IsInverted;

        return true ^ IsInverted;
    }
}
