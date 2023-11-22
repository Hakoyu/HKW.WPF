using System.Windows.Data;

namespace VPet.ModMaker.Converters;

/// <summary>
/// 相等转换器
/// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource RatioMarginConverter}">
///   <Binding Path="Value1" />
///   <Binding Path="Value2" />
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
public class EqualsConverter : IMultiValueConverter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="values"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object Convert(
        object[] values,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (values.Length != 2)
            throw new NotImplementedException("Values length must be 2");
        return values[0].Equals(values[1]);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetTypes"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object[] ConvertBack(
        object value,
        Type[] targetTypes,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
