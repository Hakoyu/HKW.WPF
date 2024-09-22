using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到枚举信息转换器
/// </summary>
public class EnumToEnumInfoConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumToEnumInfoConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumToEnumInfoConverter()
        {
            GetEnumInfoTarget = () => EnumInfoTarget,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<EnumInfoTarget> TrueValueProperty =
        CommonDependencyProperty.Register<EnumToEnumInfoConverter, EnumInfoTarget>(
            nameof(EnumInfoTarget),
            EnumInfoTarget.Name
        );

    /// <summary>
    /// 枚举信息目标
    /// </summary>
    public EnumInfoTarget EnumInfoTarget
    {
        get => GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }
}
