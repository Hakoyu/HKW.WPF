namespace HKW.WPF.Converters;

/// <summary>
/// 全部为真转换器
/// </summary>
public class AllIsTrueConverter : AllEqualsValueConverter<bool, AllIsTrueConverter>
{
    /// <inheritdoc/>
    public AllIsTrueConverter()
    {
        Value = true;
    }
}
