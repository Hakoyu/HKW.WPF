using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.EnumToEnumInfoDisplayConverter"/>
public class EnumToEnumInfoDisplayConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumToEnumInfoDisplayConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumToEnumInfoDisplayConverter()
        {
            GetEnumInfoDisplayTarget = () => EnumInfoDisplayTarget,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<EnumInfoDisplayTarget> EnumInfoDisplayTargetProperty =
        CommonDependencyProperty.Register<EnumToEnumInfoDisplayConverter, EnumInfoDisplayTarget>(
            nameof(EnumInfoDisplayTarget),
            CommonValueConverters.EnumToEnumInfoDisplayConverter.DefaultEnumInfoDisplayTarget
        );

    /// <summary>
    /// 枚举信息目标
    /// </summary>
    public EnumInfoDisplayTarget EnumInfoDisplayTarget
    {
        get => GetValue(EnumInfoDisplayTargetProperty);
        set => SetValue(EnumInfoDisplayTargetProperty, value);
    }
}
