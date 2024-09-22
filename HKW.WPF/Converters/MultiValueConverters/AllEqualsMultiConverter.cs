using System.ComponentModel;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部相等于值转换器
/// </summary>
public class AllEqualsMultiConverter<T> : InvertibleMultiValueConverterBase
{
    /// <inheritdoc/>
    public AllEqualsMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.AllEqualsMultiConverter<T>()
        {
            GetValue = () => Value
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> ValueProperty =
        CommonDependencyProperty.Register<AllEqualsMultiConverter<T>, T>(nameof(Value));

    /// <summary>
    /// 值
    /// </summary>
    public T Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
