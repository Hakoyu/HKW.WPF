using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到参数值转换器
/// </summary>
public abstract class BoolToSplitParameterConverterBase : ValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<char> SeparatorProperty =
        CommonDependencyProperty.Register<BoolToSplitParameterConverterBase, char>(
            nameof(Separator),
            ','
        );

    /// <summary>
    /// 分割符
    /// </summary>
    public char Separator
    {
        get => GetValue(SeparatorProperty);
        set => SetValue(SeparatorProperty, value);
    }

    /// <inheritdoc/>
    public override void CommonValueConverterInitialize(
        CommonValueConverters.ValueConverterBase commonValueConverter
    )
    {
        base.CommonValueConverterInitialize(commonValueConverter);
        if (
            commonValueConverter
            is not CommonValueConverters.BoolToSplitParameterConverterBase converterBase
        )
            return;
        converterBase.GetSeparator = () => Separator;
    }
}
