using System.Diagnostics.CodeAnalysis;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 尝试寻找父级
    /// </summary>
    /// <typeparam name="T">父级类型</typeparam>
    /// <param name="reference">源控件</param>
    /// <param name="outValue">找到的父级</param>
    /// <returns>找到为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public static bool TryFindParent<T>(
        this DependencyObject reference,
        [NotNullWhen(true)] out DependencyObject? outValue
    )
    {
        var result = reference.FindParent<T>();
        if (result is not null)
        {
            outValue = result;
            return true;
        }
        outValue = null;
        return false;
    }
}
