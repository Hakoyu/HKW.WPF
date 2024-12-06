using System.Globalization;
using System.Windows.Data;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 值转换器基类
/// </summary>
public abstract class ValueConverterBase : ConverterBase, IValueConverter
{
    private CommonValueConverters.ValueConverterBase? _commonValueConverter;

    /// <summary>
    /// 通用值转换器
    /// </summary>
    public CommonValueConverters.ValueConverterBase? CommonValueConverter
    {
        get => _commonValueConverter;
        set => CommonValueConverterInitialize(_commonValueConverter = value!);
    }

    /// <summary>
    /// 通用值转换器初始化
    /// </summary>
    /// <param name="commonValueConverter">通用值转换器</param>
    public virtual void CommonValueConverterInitialize(
        CommonValueConverters.ValueConverterBase commonValueConverter
    )
    {
        commonValueConverter.GetDefaultResult = () => DefaultResult;
    }

    /// <inheritdoc/>
    public virtual object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        var result = CommonValueConverter?.Convert(value, targetType, parameter, culture);
        if (ResultToString)
            return result?.ToString();
        return result;
    }

    /// <inheritdoc/>
    public virtual object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        var result = CommonValueConverter?.ConvertBack(value, targetType, parameter, culture);
        if (ResultToString)
            return result?.ToString();
        return result;
    }

    object? IValueConverter.Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return Convert(
            value,
            targetType,
            parameter,
            ValueConvertersConfig.SelectCulture(PreferredCulture, () => culture)
        );
    }

    object? IValueConverter.ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return ConvertBack(
            value,
            targetType,
            parameter,
            ValueConvertersConfig.SelectCulture(PreferredCulture, () => culture)
        );
    }
}
