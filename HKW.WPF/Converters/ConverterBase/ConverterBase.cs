using System.Globalization;
using ValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 值转换器基础
/// </summary>
public abstract class ConverterBase : DependencyObject
{
    /// <summary>
    /// 未设置值
    /// </summary>
    public static readonly object UnsetValue = DependencyProperty.UnsetValue;

    /// <summary>
    /// 首选文化
    /// </summary>
    public PreferredCulture PreferredCulture { get; set; } =
        ValueConvertersConfig.DefaultPreferredCulture;

    /// <summary>
    /// 选择文化
    /// </summary>
    /// <param name="converterCulture">转换器文化</param>
    /// <returns>文化</returns>
    protected CultureInfo SelectCulture(Func<CultureInfo> converterCulture)
    {
        return PreferredCulture switch
        {
            PreferredCulture.CurrentCulture => CultureInfo.CurrentCulture,
            PreferredCulture.CurrentUICulture => CultureInfo.CurrentUICulture,
            _ => converterCulture(),
        };
    }
}
