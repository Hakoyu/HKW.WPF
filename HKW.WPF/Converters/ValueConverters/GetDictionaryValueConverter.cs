using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Converters.ValueConverters;

/// <summary>
/// 获取字典值
/// </summary>
public class GetDictionaryValueConverter : ValueConverterBase<GetDictionaryValueConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is not IDictionary dictionary)
            return null;
        if (parameter is null)
            return null;
        if (dictionary.Contains(parameter) is false)
            return null;
        return dictionary[parameter];
    }
}
