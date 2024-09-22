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
public class GetDictionaryValueMulitiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public GetDictionaryValueMulitiConverter()
    {
        CommonValueConverter = new CommonValueConverters.GetDictionaryValueMulitiConverter();
    }
}
