using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 可反转的多值转换器
/// </summary>
public abstract class InvertibleMultiValueConverterBase : MultiValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> IsInvertedProperty =
        CommonDependencyProperty.Register<InvertibleMultiValueConverterBase, bool>(
            nameof(IsInverted)
        );

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }

    /// <inheritdoc/>
    public override void InitializeValueConverter(
        CommonValueConverters.MultiValueConverterBase commonValueConverter
    )
    {
        base.InitializeValueConverter(commonValueConverter);
        if (
            commonValueConverter
            is not CommonValueConverters.InvertibleMultiValueConverterBase converterBase
        )
            return;
        converterBase.GetIsInverted = () => IsInverted;
    }
}
