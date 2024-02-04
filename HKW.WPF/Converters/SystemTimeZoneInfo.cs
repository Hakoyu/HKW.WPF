using System;
using System.Threading;

namespace HKW.WPF.Converters;

/// <summary>
/// 系统时区信息
/// </summary>
internal class SystemTimeZoneInfo : ITimeZoneInfo
{
    /// <summary>
    /// 当前时区
    /// </summary>
    public static ITimeZoneInfo Current { get; } =
        new Lazy<ITimeZoneInfo>(
            () => new SystemTimeZoneInfo(),
            LazyThreadSafetyMode.PublicationOnly
        ).Value;

    /// <summary>
    /// UTC时区
    /// </summary>
    public TimeZoneInfo Utc => TimeZoneInfo.Utc;

    /// <summary>
    /// 本地时区
    /// </summary>
    public TimeZoneInfo Local => TimeZoneInfo.Local;
}
