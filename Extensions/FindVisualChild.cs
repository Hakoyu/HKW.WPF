using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
