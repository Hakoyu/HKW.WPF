using System.ComponentModel;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部为布尔值到值转换器
/// </summary>
public class AllBoolToValueMultiConverter<T> : InvertibleMultiValueConverterBase
{
    /// <inheritdoc/>
    public AllBoolToValueMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.AllBoolToValueMultiConverter<T>()
        {
            GetTrueValue = () => TrueValue,
            GetFalseValue = () => FalseValue,
            GetNullValue = () => NullValue
        };
    }

    /// <inheritdoc/>
    public override void InitializeValueConverter(
        CommonValueConverters.MultiValueConverterBase commonValueConverter
    )
    {
        commonValueConverter.GetDefaultResult = () => DefaultResult;
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> TrueValueProperty =
        CommonDependencyProperty.Register<AllBoolToValueMultiConverter<T>, T>(nameof(TrueValue));

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
        CommonDependencyProperty.Register<AllBoolToValueMultiConverter<T>, T>(nameof(FalseValue));

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
        CommonDependencyProperty.Register<AllBoolToValueMultiConverter<T>, T>(nameof(NullValue));

    /// <summary>
    /// 为空时的值
    /// </summary>
    public T NullValue
    {
        get => GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public new static readonly CommonDependencyProperty<T> DefaultResultProperty =
        CommonDependencyProperty.Register<AllBoolToValueMultiConverter<T>, T>(
            nameof(DefaultResult)
        );

    /// <summary>
    /// 为真时的值
    /// </summary>
    public new T DefaultResult
    {
        get => GetValue(DefaultResultProperty);
        set => SetValue(DefaultResultProperty, value);
    }
}
