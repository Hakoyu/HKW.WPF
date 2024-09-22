//using System;
//using System.Globalization;
//using System.Windows;

//namespace HKW.WPF.Converters;

///// <summary>
///// 值到布尔转换器
///// </summary>
///// <typeparam name="T">值类型</typeparam>
///// <typeparam name="TConverter">转换器类型</typeparam>
//public abstract class ValueToBoolConverterBase<T> : ValueConverterBase
//{
//    /// <summary>
//    /// 真值
//    /// </summary>
//    public abstract T TrueValue { get; set; }

//    /// <summary>
//    ///
//    /// </summary>
//    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
//        nameof(IsInverted),
//        typeof(bool),
//        typeof(ValueToBoolConverterBase<T>)
//    );

//    /// <summary>
//    /// 是反转的
//    /// </summary>
//    public bool IsInverted
//    {
//        get => (bool)GetValue(IsInvertedProperty);
//        set => SetValue(IsInvertedProperty, value);
//    }

//    /// <inheritdoc/>
//    public override object? Convert(
//        object? value,
//        Type? targetType,
//        object? parameter,
//        CultureInfo? culture
//    )
//    {
//        var trueValue = TrueValue;
//        return Equals(value, trueValue) ^ IsInverted;
//    }
//}
