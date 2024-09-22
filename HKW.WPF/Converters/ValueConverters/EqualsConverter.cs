using System.Globalization;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等转换器
/// <para>示例:
/// <code><![CDATA[
/// {Binding Value, Converter={StaticResource EqualsConverter}, ConverterParameter={x:Null}}
/// result: Value.Equals(ConverterParameter)
/// ]]></code></para>
/// </summary>
public class EqualsConverter<T> : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public EqualsConverter()
    {
        CommonValueConverter = new CommonValueConverters.EqualsConverter<T>()
        {
            GetIsStringEquals = () => IsStringEquals
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
