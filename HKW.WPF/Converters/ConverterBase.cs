using System.Windows;
using System.Windows.Data;
using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 转换器基类
/// </summary>
public abstract class ConverterBase : DependencyObject
{
    /// <summary>
    /// 未设置值
    /// </summary>
    public static object UnsetValue { get; } = DependencyProperty.UnsetValue;

    /// <summary>
    /// Allows to override the default culture used in <seealso cref="IValueConverter"/> for the current converter.
    /// The default override behavior can be configured in <seealso cref="ValueConvertersConfig.DefaultPreferredCulture"/>.
    /// </summary>
    public PreferredCulture PreferredCulture { get; set; } =
        ValueConvertersConfig.DefaultPreferredCulture;

    /// <summary>
    /// 选择文化
    /// </summary>
    /// <param name="converterCulture"></param>
    /// <returns></returns>
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
