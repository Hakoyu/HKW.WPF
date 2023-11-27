using System.Diagnostics.CodeAnalysis;

namespace HKW.WPF.Extensions;

/// <summary>
/// WPF拓展
/// </summary>
public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找父级
    /// </summary>
    /// <typeparam name="T">父级类型</typeparam>
    /// <param name="frameworkElement">源控件</param>
    /// <returns>指定类型的父级</returns>
    public static T FindParent<T>(this FrameworkElement frameworkElement)
        where T : FrameworkElement
    {
        var temp = (FrameworkElement)frameworkElement.Parent;
        while (temp is not null)
        {
            if (temp is T t)
                return t;
            temp = (FrameworkElement)temp.Parent;
        }
        return null!;
    }

    /// <summary>
    /// 尝试寻找父级
    /// </summary>
    /// <typeparam name="T">父级类型</typeparam>
    /// <param name="frameworkElement">源控件</param>
    /// <param name="outValue">找到的父级</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindParent<T>(
        this FrameworkElement frameworkElement,
        [MaybeNullWhen(false)] out T outValue
    )
        where T : FrameworkElement
    {
        var result = frameworkElement.FindParent<T>();
        if (result is not null)
        {
            outValue = result;
            return true;
        }
        outValue = null;
        return false;
    }
}
