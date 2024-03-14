namespace HKW.WPF.Converters;

internal static class ConverterUtils
{
    public static bool GetBool(object? value, bool nullValue = false)
    {
        if (value is null || value == ConverterBase.UnsetValue)
            return nullValue;
        else if (value is bool boolValue)
            return boolValue;
        else if (value is string str && bool.TryParse(str, out boolValue))
            return boolValue;
        else
            return nullValue;
    }
}
