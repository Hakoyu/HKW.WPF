//using System;
//using System.Globalization;
//using System.Windows;
//using HKW.CommonValueConverters;

//namespace HKW.WPF.Converters;

///// <summary>
///// 日期时间偏移到字符串转换器
///// </summary>
//public class DateTimeOffsetToStringConverter : ValueConverterBase
//{
//    /// <summary>
//    /// 默认格式化
//    /// </summary>
//    protected const string DefaultFormat = "g";

//    /// <summary>
//    /// 默认最小值
//    /// </summary>
//    protected const string DefaultMinValueString = "";

//    private readonly ITimeZoneInfo _timeZone;

//    /// <inheritdoc/>
//    public DateTimeOffsetToStringConverter()
//        : this(SystemTimeZoneInfo.Current) { }

//    internal DateTimeOffsetToStringConverter(ITimeZoneInfo timeZone)
//    {
//        _timeZone = timeZone;
//    }

//    /// <summary>
//    ///
//    /// </summary>
//    public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
//        nameof(Format),
//        typeof(string),
//        typeof(DateTimeOffsetToStringConverter),
//        new(DefaultFormat)
//    );

//    /// <summary>
//    /// 日期时间格式化
//    /// <para>
//    /// 格式化参考: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
//    /// </para>
//    /// </summary>
//    public string Format
//    {
//        get => (string)GetValue(FormatProperty);
//        set => SetValue(FormatProperty, value);
//    }

//    /// <summary>
//    ///
//    /// </summary>
//    public static readonly DependencyProperty MinValueStringProperty = DependencyProperty.Register(
//        nameof(MinValueString),
//        typeof(string),
//        typeof(DateTimeOffsetToStringConverter),
//        new(DefaultMinValueString)
//    );

//    /// <summary>
//    /// 最小值
//    /// </summary>
//    public string MinValueString
//    {
//        get => (string)GetValue(MinValueStringProperty);
//        set => SetValue(MinValueStringProperty, value);
//    }

//    /// <inheritdoc/>
//    public override object? Convert(
//        object? value,
//        Type? targetType,
//        object? parameter,
//        CultureInfo? culture
//    )
//    {
//        if (value is DateTimeOffset dateTimeOffset)
//        {
//            if (dateTimeOffset == DateTimeOffset.MinValue)
//            {
//                return MinValueString;
//            }

//            return TimeZoneInfo
//                .ConvertTime(dateTimeOffset, _timeZone.Local)
//                .ToString(Format, culture);
//        }

//        return null;
//    }

//    /// <inheritdoc/>
//    public override object? ConvertBack(
//        object? value,
//        Type? targetType,
//        object? parameter,
//        CultureInfo? culture
//    )
//    {
//        if (value != null)
//        {
//            if (value is DateTimeOffset dateTimeOffset)
//            {
//                return dateTimeOffset;
//            }

//            if (value is string str)
//            {
//                if (DateTimeOffset.TryParse(str, out var parsedDateTimeOffset))
//                {
//                    return TimeZoneInfo.ConvertTime(parsedDateTimeOffset, _timeZone.Utc);
//                }
//            }
//        }

//        return null;
//    }
//}
