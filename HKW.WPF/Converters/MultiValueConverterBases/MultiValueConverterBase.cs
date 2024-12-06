using System.Globalization;
using System.Windows.Data;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 多个值转换器
/// </summary>
public abstract class MultiValueConverterBase : ConverterBase, IMultiValueConverter
{
    private CommonValueConverters.MultiValueConverterBase? _commonValueConverter;

    /// <summary>
    /// 通用值转换器
    /// </summary>
    public CommonValueConverters.MultiValueConverterBase? CommonValueConverter
    {
        get => _commonValueConverter;
        set => InitializeValueConverter(_commonValueConverter = value!);
    }

    /// <summary>
    /// 通用值转换器初始化
    /// </summary>
    /// <param name="commonValueConverter">通用值转换器</param>
    public virtual void InitializeValueConverter(
        CommonValueConverters.MultiValueConverterBase commonValueConverter
    )
    {
        commonValueConverter.GetDefaultResult = () => DefaultResult;
    }

    /// <inheritdoc/>
    public virtual object? Convert(
        object?[] value,
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
    public virtual object[] ConvertBack(
        object? value,
        Type?[] targetTypes,
        object? parameter,
        CultureInfo? culture
    )
    {
        throw new NotImplementedException();
    }

    object IMultiValueConverter.Convert(
        object[] value,
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
        )!;
    }

    object[] IMultiValueConverter.ConvertBack(
        object value,
        Type[] targetTypes,
        object parameter,
        CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
