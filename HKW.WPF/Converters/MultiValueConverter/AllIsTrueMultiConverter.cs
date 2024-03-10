namespace HKW.WPF.Converters;

/// <summary>
/// 全部为真转换器
/// </summary>
public class AllIsTrueMultiConverter : AllEqualsValueMultiConverter<bool, AllIsTrueMultiConverter>
{
    /// <inheritdoc/>
    public AllIsTrueMultiConverter()
    {
        Value = true;
    }
}
