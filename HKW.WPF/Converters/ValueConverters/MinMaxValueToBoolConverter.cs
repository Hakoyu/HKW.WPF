using System.Windows;
using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 最大最小值转换器
/// <para>
/// 检查数值是否在 MinValue 和 MaxValue 之间
/// </para>
/// <para>
/// 如果值在范围内，则返回 <see langword="true"/> 如果值不在范围内，则返回 <see langword="false"/>。
/// </para>
/// <para>
/// 所有涉及的值（转换器参数 "value"、MinValue 和 MaxValue）必须是同一类型。
/// </para>
/// <para>
/// 并且必须实现 <seealso cref="IComparable"/> (https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)。
/// </para>
/// </summary>
public class MinMaxValueToBoolConverter : ValueConverterBase<MinMaxValueToBoolConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
        nameof(MaxValue),
        typeof(object),
        typeof(MinMaxValueToBoolConverter)
    );

    /// <summary>
    /// 最大值
    /// </summary>
    public object MaxValue
    {
        get => GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
        nameof(MinValue),
        typeof(object),
        typeof(MinMaxValueToBoolConverter)
    );

    /// <summary>
    /// 最小值
    /// </summary>
    public object MinValue
    {
        get => GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

#pragma warning disable CA2208 // 正确实例化参数异常
    /// <inheritdoc/>
    public override object? Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (value is not IComparable comparable)
        {
            return null;
        }

        if (MinValue is not IComparable)
        {
            throw new ArgumentException(
                "MinValue must implement IComparable interface",
                nameof(MinValue)
            );
        }

        if (MaxValue is not IComparable)
        {
            throw new ArgumentException(
                "MaxValue must implement IComparable interface",
                nameof(MaxValue)
            );
        }

        var minValue = System.Convert.ChangeType(MinValue, comparable.GetType());
        var maxValue = System.Convert.ChangeType(MaxValue, comparable.GetType());

        return comparable.CompareTo(minValue) >= 0 && comparable.CompareTo(maxValue) <= 0;
    }
#pragma warning restore CA2208 // 正确实例化参数异常
}
