using System.Numerics;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 在范围里转换器
/// </summary>
public class NumberClampConverter<T> : ValueConverterBase
    where T : struct, INumber<T>
{
    /// <inheritdoc/>
    public NumberClampConverter()
    {
        CommonValueConverter = new CommonValueConverters.NumberClampConverter<T>()
        {
            GetMaxValue = () => MaxValue,
            GetMinValue = () => MinValue,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> MaxValueProperty =
        CommonDependencyProperty.Register<GuidToStringConverter, T>(nameof(MaxValue));

    /// <summary>
    /// 格式化
    /// </summary>
    public T MaxValue
    {
        get => GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> MinValueProperty =
        CommonDependencyProperty.Register<GuidToStringConverter, T>(nameof(MinValue));

    /// <summary>
    /// 格式化
    /// </summary>
    public T MinValue
    {
        get => GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }
}
