using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 可反转的值转换器基类
/// </summary>
/// <typeparam name="TConverter">转换器类型</typeparam>
public abstract class InvertibleValueConverterBase<TConverter> : ValueConverterBase<TConverter>
    where TConverter : InvertibleValueConverterBase<TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(InvertibleValueConverterBase<TConverter>)
    );

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => (bool)GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }
}
