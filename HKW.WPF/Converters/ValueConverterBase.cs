using System.Globalization;
using System.Windows.Data;

namespace HKW.WPF.Converters;

/// <summary>
/// 值转换器基类
/// </summary>
/// <typeparam name="TConverter">转换类型</typeparam>
public abstract class ValueConverterBase<TConverter> : ConverterBase, IValueConverter
    where TConverter : ValueConverterBase<TConverter>, new()
{
    private static readonly Lazy<TConverter> InstanceConstructor =
        new(() => new(), LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// 单例
    /// </summary>
    public static TConverter Instance => InstanceConstructor.Value;

#if DEBUG
    /// <inheritdoc/>
    public ValueConverterBase()
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
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    );

    /// <inheritdoc/>
    public virtual object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        throw new NotSupportedException(
            $"Converter '{GetType().Name}' does not support backward conversion."
        );
    }

    object? IValueConverter.Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return Convert(value, targetType, parameter, SelectCulture(() => culture))?.ToString();
    }

    object? IValueConverter.ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return ConvertBack(value, targetType, parameter, SelectCulture(() => culture));
    }
}
