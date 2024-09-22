using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public class BoolToValueConverter<T> : InvertibleValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> TrueValueProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, T>(nameof(TrueValue));

    /// <summary>
    /// 为真时的值
    /// </summary>
    public T TrueValue
    {
        get => GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> FalseValueProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, T>(nameof(FalseValue));

    /// <summary>
    /// 为假时的值
    /// </summary>
    public T FalseValue
    {
        get => GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> NullValueProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, T>(nameof(NullValue));

    /// <summary>
    /// 为空时的布尔值
    /// </summary>
    public T NullValue
    {
        get => GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }

    /// <inheritdoc/>
    public override void CommonValueConverterInitialize(
        CommonValueConverters.ValueConverterBase commonValueConverter
    )
    {
        base.CommonValueConverterInitialize(commonValueConverter);
        if (commonValueConverter is not CommonValueConverters.BoolToValueConverter<T> converter)
            return;
        converter.GetTrueValue = () => TrueValue;
        converter.GetFalseValue = () => FalseValue;
        converter.GetNullValue = () => NullValue;
    }
}
