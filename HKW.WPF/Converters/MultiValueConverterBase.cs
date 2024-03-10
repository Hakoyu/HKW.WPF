using System.Globalization;
using System.Windows.Data;

namespace HKW.WPF.Converters;

/// <summary>
/// 多个值转换器
/// </summary>
/// <typeparam name="TConverter">转换器类型</typeparam>
public abstract class MultiValueConverterBase<TConverter> : ConverterBase, IMultiValueConverter
    where TConverter : MultiValueConverterBase<TConverter>, new()
{
    /// <summary>
    /// 单例
    /// </summary>
    public static TConverter Instance { get; } =
        new Lazy<TConverter>(() => new TConverter(), LazyThreadSafetyMode.PublicationOnly).Value;

#if DEBUG
    /// <inheritdoc/>
    public MultiValueConverterBase()
    {
        var sourceType = GetType();
        var instanceType = typeof(TConverter);
        if (sourceType != instanceType)
            throw new InvalidCastException(
                $"Instance type error\nSource type:{sourceType.FullName}\nInstance type:{instanceType.FullName}"
            );
    }
#endif

    /// <inheritdoc/>
    public abstract object? Convert(
        object[] value,
        Type targetType,
        object parameter,
        CultureInfo culture
    );

    /// <inheritdoc/>
    public virtual object[] ConvertBack(
        object value,
        Type[] targetTypes,
        object parameter,
        CultureInfo culture
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
        return Convert(value, targetType, parameter, SelectCulture(() => culture))!;
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
