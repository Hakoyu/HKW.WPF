using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 值到布尔转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public class ValueToBoolConverter<T>
    : ReversibleValueToBoolConverterBase<T, ValueToBoolConverter<T>>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
        nameof(TrueValue),
        typeof(T),
        typeof(ValueToBoolConverter<T>)
    );

    /// <summary>
    /// 为真时的值
    /// </summary>
    public override T TrueValue
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
        typeof(ValueToBoolConverter<T>)
    );

    /// <summary>
    /// 为假时的值
    /// </summary>
    public override T FalseValue
    {
        get => (T)GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }
}

/// <summary>
/// 值到布尔转换器
/// </summary>
public class ValueToBoolConverter : ValueToBoolConverter<object> { }
