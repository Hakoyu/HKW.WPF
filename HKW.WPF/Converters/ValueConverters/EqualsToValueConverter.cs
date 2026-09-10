using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等到可见性转换器
/// <para>示例:
/// <code><![CDATA[
/// {Binding Value, Converter={StaticResource EqualsToVisibilityConverter}}
/// result: Value is null ? NullValue : (Value.Equals(ConverterParameter) ? TrueValue : FalseValue)
/// ]]></code></para>
public class EqualsToVisibilityConverter : EqualsToValueConverter<Visibility>
{
    /// <inheritdoc/>
    public EqualsToVisibilityConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
        NullValue = Visibility.Collapsed;
    }
}

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
            GetOther = () => Other,
            GetTrueValue = () => TrueValue,
            GetFalseValue = () => FalseValue,
            GetNullValue = () => NullValue,
            GetIsStringEquals = () => IsStringEquals,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> OtherProperty =
        CommonDependencyProperty.Register<EqualsToValueConverter<T>, T>(nameof(Other));

    /// <summary>
    /// 其他值
    /// </summary>
    public T Other
    {
        get => GetValue(OtherProperty);
        set => SetValue(OtherProperty, value);
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
        CommonDependencyProperty.Register<EqualsToValueConverter<T>, bool>(nameof(IsStringEquals));

    /// <summary>
    /// 是字符串比较
    /// </summary>
    public bool IsStringEquals
    {
        get => GetValue(IsStringEqualsProperty);
        set => SetValue(IsStringEqualsProperty, value);
    }
}
