//using System;
//using System.Globalization;
//using System.Windows;
//using HKW.CommonValueConverters;
//using HKW.WPF;

//namespace HKW.WPF.Converters;

///// <summary>
///// 日期时间到字符串转换器
///// </summary>
//public class DateTimeToStringConverter : ValueConverterBase
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
//    public DateTimeToStringConverter()
//        : this(SystemTimeZoneInfo.Current) { }

//    internal DateTimeToStringConverter(ITimeZoneInfo timeZone)
//    {
//        _timeZone = timeZone;
//    }

//    /// <summary>
//    ///
//    /// </summary>
//    public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
//        nameof(Format),
//        typeof(string),
//        typeof(DateTimeToStringConverter),
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
//        typeof(DateTimeToStringConverter),
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
//        if (value is DateTime dateTime)
//        {
//            if (dateTime == DateTime.MinValue)
//            {
//                return MinValueString;
//            }

//            var localDateTime = TimeZoneInfo.ConvertTime(dateTime, _timeZone.Local);
//            return localDateTime.ToString(Format, culture);
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
//            if (value is DateTime dateTime)
//            {
//                return dateTime;
//            }

//            if (value is string str)
//            {
//                if (DateTime.TryParse(str, out var parsedDateTime))
//                {
//                    return TimeZoneInfo.ConvertTime(parsedDateTime, _timeZone.Utc);
//                }
//            }
//        }

//        return null;
//    }
//}
