using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace HKW.WPF.Extensions;

/// <summary>
/// WPF拓展
/// </summary>
public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找父元素
    /// </summary>
    /// <typeparam name="T">父元素类型</typeparam>
    /// <param name="element">源控件</param>
    /// <returns>父元素</returns>
    public static T FindParent<T>(this FrameworkElement element)
        where T : FrameworkElement
    {
        var parent = element.Parent as FrameworkElement;
        while (parent is not null)
        {
            if (parent is T t)
                return t;
            parent = parent.Parent as FrameworkElement;
        }
        return null!;
    }

    /// <summary>
    /// 尝试寻找父元素
    /// </summary>
    /// <typeparam name="T">父元素类型</typeparam>
    /// <param name="element">源控件</param>
    /// <param name="parent">父元素</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindParent<T>(
        this FrameworkElement element,
        [MaybeNullWhen(false)] out T parent
    )
        where T : FrameworkElement
    {
        var result = element.FindParent<T>();
        if (result is not null)
        {
            parent = result;
            return true;
        }
        parent = null;
        return false;
    }
}
