using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace HKW.WPF.Extensions;

/// <summary>
/// WPF拓展
/// </summary>
public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找视觉父级
    /// </summary>
    /// <typeparam name="T">父级类型</typeparam>
    /// <param name="reference">源控件</param>
    /// <returns>指定类型的父级</returns>
    public static T FindVisuaParent<T>(this DependencyObject reference)
        where T : DependencyObject
    {
        var temp = reference;
        while ((temp = VisualTreeHelper.GetParent(temp)) is not null)
        {
            if (temp is T t)
                return t;
        }
        return null!;
    }

    /// <summary>
    /// 尝试寻找视觉父级
    /// </summary>
    /// <typeparam name="T">父级类型</typeparam>
    /// <param name="reference">源控件</param>
    /// <param name="outValue">找到的父级</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindVisuaParent<T>(
        this DependencyObject reference,
        [MaybeNullWhen(false)] out T outValue
    )
        where T : DependencyObject
    {
        var result = reference.FindVisuaParent<T>();
        if (result is not null)
        {
            outValue = result;
            return true;
        }
        outValue = null;
        return false;
    }
}
