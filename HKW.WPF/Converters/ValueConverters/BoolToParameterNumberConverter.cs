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
        converterBase.GetConvertParameter = s => new Thickness(double.Parse(s));
    }
}

///// <summary>
///// bool到数字转换器
///// <para>示例:
///// <code><![CDATA[
///// <Binding Bool, Converter="{StaticResource BoolToParameterDoubleConverter}" ConverterParameter="1,2"/>
///// result: Bool ? 1 : 2
///// ]]></code></para>
///// </summary>
//public class BoolToParameterNumberConverter<T> : BoolToSplitParameterConverterBase
//    where T : struct, INumber<T>
//{
//    /// <inheritdoc/>
//    public BoolToParameterNumberConverter()
//    {
//        CommonValueConverter = new CommonValueConverters.BoolToSplitNumberConverter<T>();
//    }
//}

/// <summary>
/// bool到参数分割值转换器
/// </summary>
public class BoolToSplitParameterConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public BoolToSplitParameterConverter()
    {
        CommonValueConverter = new CommonValueConverters.BoolToSplitParameterConverter();
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<char> SeparatorProperty =
        CommonDependencyProperty.Register<BoolToSplitParameterConverter, char>(
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
}
