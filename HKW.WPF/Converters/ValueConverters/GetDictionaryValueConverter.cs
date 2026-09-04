using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Converters.ValueConverters;

/// <inheritdoc cref="CommonValueConverters.GetDictionaryValueConverter"/>
public class GetDictionaryValueConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public GetDictionaryValueConverter()
    {
        CommonValueConverter = new CommonValueConverters.GetDictionaryValueConverter();
    }
}
