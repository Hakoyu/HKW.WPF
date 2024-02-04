namespace HKW.WPF.Converters;

/// <summary>
/// 布尔反转转换器
/// </summary>
public class BoolInverteConverter : BoolToValueConverter<bool, BoolInverteConverter>
{
    /// <inheritdoc/>
    public BoolInverteConverter()
    {
        TrueValue = true;
        FalseValue = false;
        IsInverted = true;
    }
}
