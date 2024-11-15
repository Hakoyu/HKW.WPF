namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到枚举信息转换器
/// </summary>
public class EnumsToEnumInfosConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumsToEnumInfosConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumsToEnumInfosConverter();
    }
}
