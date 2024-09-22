using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部相等于值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public class AnyEqualsMultiConverter<T> : InvertibleMultiValueConverterBase
{
    /// <inheritdoc/>
    public AnyEqualsMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.AnyEqualsMultiConverter<T>()
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
