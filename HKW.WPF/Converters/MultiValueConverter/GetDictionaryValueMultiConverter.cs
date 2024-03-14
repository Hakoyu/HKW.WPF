using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Converters.MultiValueConverter;

/// <summary>
/// 获取字典值
/// </summary>
public class GetDictionaryValueMulitiConverter
    : MultiValueConverterBase<GetDictionaryValueMulitiConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object?[] value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (parameter is null)
        {
            if (value.Length != 2)
                return null;
            if (value[0] is not IDictionary dictionary)
                return null;
            if (value[1] is null)
                return null;
            if (dictionary.Contains(value[1]) is false)
                return null;
            return dictionary[value[1]];
        }
        else
        {
            if (value.Length == 1)
                return null;
            if (value[0] is not IDictionary dictionary)
                return null;
            if (parameter is null)
                return null;
            if (dictionary.Contains(parameter) is false)
                return null;
            return dictionary[parameter];
        }
    }
}
