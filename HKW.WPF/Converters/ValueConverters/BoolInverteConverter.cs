namespace HKW.WPF.Converters;

/// <summary>
/// 布尔反转转换器
/// </summary>
public class BoolInverteConverter : BoolToValueConverter<bool>
{
    /// <inheritdoc/>
    public BoolInverteConverter()
    {
        TrueValue = true;
        FalseValue = false;
        NullValue = false;
        IsInverted = true;
    }
}
