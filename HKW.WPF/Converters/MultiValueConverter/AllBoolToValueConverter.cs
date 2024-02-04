using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部为布尔值到值转换器
/// </summary>
public class AllBoolToValueConverter<T, TConverter>
    : MultiValueConverterBase<AllBoolToValueConverter<T, TConverter>>
    where TConverter : AllBoolToValueConverter<T, TConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(AllBoolToValueConverter<T, TConverter>)
    );

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => (bool)GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty NullValueProperty = DependencyProperty.Register(
        nameof(NullValue),
        typeof(bool),
        typeof(AllBoolToValueConverter<T, TConverter>),
        new(false)
    );

    /// <summary>
    /// 目标为空时的布尔值
    /// </summary>
    [DefaultValue(false)]
    public bool NullValue
    {
        get => (bool)GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
        nameof(TrueValue),
        typeof(T),
        typeof(AllBoolToValueConverter<T, TConverter>)
    );

    /// <summary>
    /// 为真时的值
    /// </summary>
    public T TrueValue
    {
        get => (T)GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty FalseValueProperty = DependencyProperty.Register(
        nameof(FalseValue),
        typeof(T),
        typeof(AllBoolToValueConverter<T, TConverter>)
    );

    /// <summary>
    /// 为假时的值
    /// </summary>
    public T FalseValue
    {
        get => (T)GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="values"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public override object? Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        var nullValue = NullValue;
        return values.All(o => ConverterUtils.GetBool(o, nullValue)) ^ IsInverted
            ? TrueValue
            : FalseValue;
    }
}
