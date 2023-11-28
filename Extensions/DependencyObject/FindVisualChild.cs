using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找视觉子项
    /// </summary>
    /// <typeparam name="T">子项类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <returns>子项</returns>
    public static T FindVisualChild<T>(this DependencyObject obj)
        where T : DependencyObject
    {
        ArgumentNullException.ThrowIfNull(obj, nameof(obj));
        var count = VisualTreeHelper.GetChildrenCount(obj);
        for (int i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            if (child is T t)
                return t;
            if (FindVisualChild<T>(child) is T childItem)
                return childItem;
        }
        return null!;
    }

    /// <summary>
    /// 寻找视觉子项
    /// </summary>
    /// <typeparam name="T">子项类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <param name="outValue">子项</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindVisualChild<T>(
        this DependencyObject obj,
        [MaybeNullWhen(false)] out T outValue
    )
        where T : DependencyObject
    {
        var result = obj.FindVisualChild<T>();
        if (result is not null)
        {
            outValue = result;
            return true;
        }
        outValue = null;
        return false;
    }
}
