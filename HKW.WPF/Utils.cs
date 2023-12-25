using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF;

internal static class Utils
{
    public static bool GetBool(object value, bool boolValue, bool nullValue)
    {
        if (value is null)
            return nullValue;
        else if (value is bool b)
            return b == boolValue;
        else if (bool.TryParse(value.ToString(), out b))
            return b == boolValue;
        else
            return false;
    }
}
