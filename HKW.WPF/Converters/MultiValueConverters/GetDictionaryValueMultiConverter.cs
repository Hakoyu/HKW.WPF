using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.GetDictionaryValueMultiConverter"/>
public class GetDictionaryValueMultiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public GetDictionaryValueMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.GetDictionaryValueMultiConverter();
    }
}
