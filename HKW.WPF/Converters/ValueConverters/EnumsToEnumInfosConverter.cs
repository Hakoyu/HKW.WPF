using System.ComponentModel;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到枚举信息转换器
/// </summary>
public class EnumsToEnumInfosConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumsToEnumInfosConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumsToEnumInfosConverter()
        {
            GetOnlyValid = () => OnlyValid
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> OnlyValidProperty =
        CommonDependencyProperty.Register<EnumsToEnumInfosConverter, bool>(
            nameof(OnlyValid),
            false
        );

    /// <summary>
    /// 只显示有效值
    /// </summary>
    [DefaultValue(true)]
    public bool OnlyValid
    {
        get => GetValue(OnlyValidProperty);
        set => SetValue(OnlyValidProperty, value);
    }
}
