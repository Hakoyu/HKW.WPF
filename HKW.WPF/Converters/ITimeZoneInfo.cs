using System;

namespace HKW.WPF.Converters;

/// <summary>
/// 时区信息接口
/// </summary>
internal interface ITimeZoneInfo
{
    /// <summary>
    /// UTC时区
    /// </summary>
    public TimeZoneInfo Utc { get; }

    /// <summary>
    /// 本地时区
    /// </summary>
    public TimeZoneInfo Local { get; }
}
