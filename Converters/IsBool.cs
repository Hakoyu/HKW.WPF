using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 是布尔转换器
/// </summary>
public class IsBoolConverter : NullToBoolConverter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    protected override object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (value.Equals(true ^ IsInverted))
            return true;
        return false;
    }
}
