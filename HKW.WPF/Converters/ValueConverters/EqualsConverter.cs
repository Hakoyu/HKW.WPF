using System.Globalization;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.EqualsConverter{T}"/>
public class EqualsConverter<T> : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public EqualsConverter()
    {
        CommonValueConverter = new CommonValueConverters.EqualsConverter<T>()
        {
            GetIsStringEquals = () => IsStringEquals,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> IsStringEqualsProperty =
        CommonDependencyProperty.Register<EqualsConverter<T>, bool>(nameof(IsStringEquals));

    /// <summary>
    /// 是字符串比较
    /// </summary>
    public bool IsStringEquals
    {
        get => GetValue(IsStringEqualsProperty);
        set => SetValue(IsStringEqualsProperty, value);
    }
}
