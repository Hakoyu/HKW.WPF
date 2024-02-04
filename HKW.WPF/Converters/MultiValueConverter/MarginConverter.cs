using System.Windows;
using System.Globalization;
using System.Windows;

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
///
/// <MultiBinding Converter="{StaticResource HasRatioMarginConverter}">
///   <Binding Path="Ratio" />
///   <Binding Path="Left" />
///   <Binding Path="Top" />
///   <Binding Path="Right" />
///   <Binding Path="Bottom" />
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
public class MarginConverter : MultiValueConverterBase<MarginConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty HasRatioProperty = DependencyProperty.Register(
        nameof(HasRatio),
        typeof(bool),
        typeof(MarginConverter)
    );

    /// <summary>
    /// 有比率
    /// </summary>
    public bool HasRatio
    {
        get => (bool)GetValue(HasRatioProperty);
        set => SetValue(HasRatioProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="values"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override object? Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (values.Any(i => i == UnsetValue))
            return new Thickness();
        if (values.Length == 0)
        {
            return new Thickness();
        }
        if (HasRatio)
        {
            if (values.Length == 1)
            {
                return new Thickness();
            }
            var ratio = System.Convert.ToDouble(values[0]);
            if (values.Length == 2)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[1]) * ratio,
                    default,
                    default,
                    default
                );
            }
            else if (values.Length == 3)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[1]) * ratio,
                    System.Convert.ToDouble(values[2]) * ratio,
                    default,
                    default
                );
            }
            else if (values.Length == 4)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[1]) * ratio,
                    System.Convert.ToDouble(values[2]) * ratio,
                    System.Convert.ToDouble(values[3]) * ratio,
                    default
                );
            }
            else if (values.Length == 5)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[1]) * ratio,
                    System.Convert.ToDouble(values[2]) * ratio,
                    System.Convert.ToDouble(values[3]) * ratio,
                    System.Convert.ToDouble(values[4]) * ratio
                );
            }
            else
                throw new NotImplementedException();
        }
        else
        {
            if (values.Length == 1)
            {
                return new Thickness(System.Convert.ToDouble(values[0]), default, default, default);
            }
            else if (values.Length == 2)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[0]),
                    System.Convert.ToDouble(values[1]),
                    default,
                    default
                );
            }
            else if (values.Length == 3)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[0]),
                    System.Convert.ToDouble(values[1]),
                    System.Convert.ToDouble(values[2]),
                    default
                );
            }
            else if (values.Length == 4)
            {
                return new Thickness(
                    System.Convert.ToDouble(values[0]),
                    System.Convert.ToDouble(values[1]),
                    System.Convert.ToDouble(values[2]),
                    System.Convert.ToDouble(values[3])
                );
            }
            else
                throw new NotImplementedException();
        }
    }
}
