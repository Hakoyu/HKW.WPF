using System.Globalization;
using System.Windows.Data;

namespace HKW.WPF.Converters;

/// <summary>
/// 边距转换器
/// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource MarginConverter}">
///   <Binding Path="Left" />
///   <Binding Path="Top" />
///   <Binding Path="Right" />
///   <Binding Path="Bottom" />
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
public class MarginConverter : IMultiValueConverter
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
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 0)
        {
            return new Thickness();
        }
        else if (values.Length == 1)
        {
            return new Thickness()
            {
                Left = System.Convert.ToDouble(values[0]),
                Top = default,
                Right = default,
                Bottom = default
            };
        }
        else if (values.Length == 2)
        {
            return new Thickness()
            {
                Left = System.Convert.ToDouble(values[0]),
                Top = System.Convert.ToDouble(values[1]),
                Right = default,
                Bottom = default
            };
        }
        else if (values.Length == 3)
        {
            return new Thickness()
            {
                Left = System.Convert.ToDouble(values[0]),
                Top = System.Convert.ToDouble(values[1]),
                Right = System.Convert.ToDouble(values[2]),
                Bottom = default
            };
        }
        else if (values.Length == 4)
        {
            return new Thickness()
            {
                Left = System.Convert.ToDouble(values[0]),
                Top = System.Convert.ToDouble(values[1]),
                Right = System.Convert.ToDouble(values[2]),
                Bottom = System.Convert.ToDouble(values[3])
            };
        }
        else
            throw new NotImplementedException();
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
        CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
