using System.Globalization;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

public class BoolInverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public BoolInverter()
    {
        CommonValueConverter = new CommonValueConverters.EqualsConverter()
        {
            GetOther = () => false,
        };
    }
}

/// <inheritdoc cref="CommonValueConverters.EqualsConverter"/>
public class EqualsConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public EqualsConverter()
    {
        CommonValueConverter = new CommonValueConverters.EqualsConverter()
        {
            GetOther = () => Other,
            GetIsStringEquals = () => IsStringEquals,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<object?> OtherProperty =
        CommonDependencyProperty.Register<EqualsConverter, object?>(nameof(Other));

    /// <summary>
    /// 其他值
    /// </summary>
    public object? Other
    {
        get => GetValue(OtherProperty);
        set => SetValue(OtherProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> IsStringEqualsProperty =
        CommonDependencyProperty.Register<EqualsConverter, bool>(nameof(IsStringEquals));

    /// <summary>
    /// 是字符串比较
    /// </summary>
    public bool IsStringEquals
    {
        get => GetValue(IsStringEqualsProperty);
        set => SetValue(IsStringEqualsProperty, value);
    }
}
