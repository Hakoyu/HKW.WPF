using System.Windows;
using System;
using System.Collections;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 集合为空转换器
/// </summary>
public class CollectionIsEmptyConverter : ValueConverterBase<CollectionIsEmptyConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(CollectionIsEmptyConverter)
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
        if (value is IEnumerable enumerable)
            return (enumerable.GetEnumerator().MoveNext() is false) ^ IsInverted;

        return true ^ IsInverted;
    }
}
