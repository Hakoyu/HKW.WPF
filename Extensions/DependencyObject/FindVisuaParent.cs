using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace HKW.WPF.Extensions;

/// <summary>
/// WPF拓展
/// </summary>
public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找视觉父元素
    /// </summary>
    /// <typeparam name="T">父元素类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <returns>父元素</returns>
    public static T FindVisuaParent<T>(this DependencyObject obj)
        where T : DependencyObject
    {
        var temp = obj;
        while ((temp = VisualTreeHelper.GetParent(temp)) is not null)
        {
            if (temp is T t)
                return t;
        }
        return null!;
    }

    /// <summary>
    /// 尝试寻找视觉父元素
    /// </summary>
    /// <typeparam name="T">父元素类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <param name="outValue">父元素</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindVisuaParent<T>(
        this DependencyObject obj,
        [MaybeNullWhen(false)] out T outValue
    )
        where T : DependencyObject
    {
        var result = obj.FindVisuaParent<T>();
        if (result is null)
        {
            outValue = null;
            return false;
        }
        outValue = result;
        return true;
    }
}
