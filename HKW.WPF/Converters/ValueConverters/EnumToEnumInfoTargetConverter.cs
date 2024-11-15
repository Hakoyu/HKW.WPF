using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到枚举信息转换器
/// </summary>
public class EnumToEnumInfoTargetConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumToEnumInfoTargetConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumToEnumInfoTargetConverter()
        {
            GetEnumInfoDisplayTarget = () => EnumInfoDisplayTarget,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<EnumInfoDisplayTarget> EnumInfoDisplayTargetProperty =
        CommonDependencyProperty.Register<EnumToEnumInfoConverter, EnumInfoDisplayTarget>(
            nameof(EnumInfoDisplayTarget),
            EnumInfoDisplayTarget.Name
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
