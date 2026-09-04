using System.Globalization;
using System.Numerics;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <summary>
/// bool到double转换器
/// </summary>
public class BoolToSplitThicknessConverter : BoolToSplitParameterConverter
{
    /// <inheritdoc/>
    public override void CommonValueConverterInitialize(
        CommonValueConverters.ValueConverterBase commonValueConverter
    )
    {
        base.CommonValueConverterInitialize(commonValueConverter);
        if (
            commonValueConverter
            is not CommonValueConverters.BoolToSplitParameterConverter converterBase
        )
            return;
        converterBase.ConvertSplitValue = s => new Thickness(double.Parse(s));
    }
}

/// <inheritdoc cref="CommonValueConverters.BoolToSplitParameterConverter"/>
public class BoolToSplitParameterConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public BoolToSplitParameterConverter()
    {
        CommonValueConverter = new CommonValueConverters.BoolToSplitParameterConverter()
        {
            GetSeparator = () => Separator,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> SeparatorProperty =
        CommonDependencyProperty.Register<BoolToSplitParameterConverter, string>(
            nameof(Separator),
            CommonValueConverters.BoolToSplitParameterConverter.DefaultSeparator
        );

    /// <summary>
    /// 分割符
    /// </summary>
    public string Separator
    {
        get => GetValue(SeparatorProperty);
        set => SetValue(SeparatorProperty, value);
    }
}
