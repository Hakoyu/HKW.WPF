using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等到值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public class EqualsToValueConverter<T> : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public EqualsToValueConverter()
    {
        CommonValueConverter = new CommonValueConverters.EqualsToValueConverter<T>()
        {
            GetTrueValue = () => TrueValue,
            GetFalseValue = () => FalseValue,
            GetIsNullable = () => IsNullable,
            GetNullValue = () => NullValue,
            GetIsStringEquals = () => IsStringEquals,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> TrueValueProperty =
        CommonDependencyProperty.Register<EqualsToValueConverter<T>, T>(nameof(TrueValue));

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
        CommonDependencyProperty.Register<EqualsToValueConverter<T>, T>(nameof(FalseValue));

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
    public static readonly CommonDependencyProperty<bool> IsNullableProperty =
        CommonDependencyProperty.Register<EqualsToValueConverter<T>, bool>(nameof(IsNullable));

    /// <summary>
    /// 是可为空的
    /// </summary>
    public bool IsNullable
    {
        get => GetValue(IsNullableProperty);
        set => SetValue(IsNullableProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> NullValueProperty =
        CommonDependencyProperty.Register<EqualsToValueConverter<T>, T>(nameof(NullValue));

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
    public static readonly CommonDependencyProperty<bool> IsStringEqualsProperty =
        CommonDependencyProperty.Register<FirstEqualsSecondMultiConverter, bool>(
            nameof(IsStringEquals)
        );

    /// <summary>
    /// 是字符串比较
    /// </summary>
    public bool IsStringEquals
    {
        get => GetValue(IsStringEqualsProperty);
        set => SetValue(IsStringEqualsProperty, value);
    }
}
