using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.FirstEqualsSecondMultiConverter"/>
public class FirstEqualsSecondMultiConverter : InvertibleMultiValueConverterBase
{
    /// <inheritdoc/>
    public FirstEqualsSecondMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.FirstEqualsSecondMultiConverter()
        {
            GetIsStringEquals = () => IsStringEquals,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> IsStringEqualsProperty =
        CommonDependencyProperty.Register<FirstEqualsSecondMultiConverter, bool>(
            nameof(IsStringEquals)
        );

    /// <summary>
    /// 是字符串比较
    /// </summary>
    public bool IsStringEquals
    {
        get => GetValue(IsStringEqualsProperty);
        set => SetValue(IsStringEqualsProperty, value);
    }
}
