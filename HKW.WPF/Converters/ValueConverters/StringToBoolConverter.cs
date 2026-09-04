using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串到布尔转换器
/// </summary>
public class ToBoolConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public ToBoolConverter()
    {
        CommonValueConverter = new CommonValueConverters.ToBoolConverter()
        {
            GetNullValue = () => NullValue,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool?> NullValueProperty =
        CommonDependencyProperty.Register<ToBoolConverter, bool?>(nameof(NullValue));

    /// <summary>
    /// 为真时的值
    /// </summary>
    public bool? NullValue
    {
        get => GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }
}
