using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 可反转的值转换器基类
/// </summary>
public abstract class InvertibleValueConverterBase : ValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> IsInvertedProperty =
        CommonDependencyProperty.Register<InvertibleValueConverterBase, bool>(nameof(IsInverted));

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }

    /// <inheritdoc/>
    public override void CommonValueConverterInitialize(
        CommonValueConverters.ValueConverterBase commonValueConverter
    )
    {
        base.CommonValueConverterInitialize(commonValueConverter);
        if (
            commonValueConverter
            is not CommonValueConverters.InvertibleValueConverterBase converterBase
        )
            return;
        converterBase.GetIsInverted = () => IsInverted;
    }
}
