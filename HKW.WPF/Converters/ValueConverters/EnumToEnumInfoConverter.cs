using System.Globalization;
using System.Windows;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到枚举信息转换器
/// </summary>
public class EnumToEnumInfoConverter : ValueConverterBase<EnumToEnumInfoConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
        nameof(EnumInfoTarget),
        typeof(EnumInfoTarget),
        typeof(EnumToEnumInfoConverter),
        new(EnumInfoTarget.Name)
    );

    /// <summary>
    /// 枚举信息目标
    /// </summary>
    public EnumInfoTarget EnumInfoTarget
    {
        get => (EnumInfoTarget)GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is not Enum @enum)
        {
            ArgumentNullException.ThrowIfNull(value);
            return null;
        }
        var type = value.GetType();
        return GetTarget(EnumInfo.GetEnumInfo(type, @enum)!, EnumInfoTarget);
    }

    /// <summary>
    /// 获取目标
    /// </summary>
    /// <param name="info">信息</param>
    /// <param name="target">目标</param>
    /// <returns>目标字符串</returns>
    public static string? GetTarget(IEnumInfo info, EnumInfoTarget target)
    {
        return target switch
        {
            EnumInfoTarget.Name => info.Name,
            EnumInfoTarget.ShortName => info.ShortName,
            EnumInfoTarget.Description => info.Description,
            _ => null,
        };
    }
}

/// <summary>
/// 枚举信息目标
/// </summary>
public enum EnumInfoTarget
{
    /// <summary>
    /// 名称
    /// </summary>
    Name,

    /// <summary>
    /// 短名称
    /// </summary>
    ShortName,

    /// <summary>
    /// 描述
    /// </summary>
    Description,
}
