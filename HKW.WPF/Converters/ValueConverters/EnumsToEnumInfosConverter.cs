using System.ComponentModel;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.EnumsToEnumInfosConverter"/>
public class EnumsToEnumInfosConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumsToEnumInfosConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumsToEnumInfosConverter()
        {
            GetOnlyValid = () => OnlyValid,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> OnlyValidProperty =
        CommonDependencyProperty.Register<EnumsToEnumInfosConverter, bool>(nameof(OnlyValid));

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
