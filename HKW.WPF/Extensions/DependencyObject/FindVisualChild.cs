using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找视觉子元素
    /// </summary>
    /// <typeparam name="T">子元素类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <returns>子元素</returns>
    public static T FindVisualChild<T>(this DependencyObject obj)
        where T : DependencyObject
    {
        ArgumentNullException.ThrowIfNull(obj);
        return FindVisualChild(obj);

        static T FindVisualChild(DependencyObject obj)
        {
            var count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is null)
                    continue;
                if (child is T t)
                    return t;
                if (FindVisualChild(child) is T childItem)
                    return childItem;
            }
            return null!;
        }
    }

    /// <summary>
    /// 寻找视图元素 <see cref="Page"/> 或 <see cref="UserControl"/>
    /// </summary>
    /// <param name="obj">源控件</param>
    /// <param name="dataContext">视图模型</param>
    /// <returns>视图</returns>
    public static DependencyObject FindView(
        this DependencyObject obj,
        INotifyPropertyChanged dataContext
    )
    {
        ArgumentNullException.ThrowIfNull(obj);
        return FindVisualChild(obj, dataContext);

        static DependencyObject FindVisualChild(
            DependencyObject obj,
            INotifyPropertyChanged dataContext
        )
        {
            var count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is null)
                    continue;
                if (child is UserControl || child is Page)
                {
                    var e = (FrameworkElement)child;
                    if (e.DataContext.Equals(dataContext))
                        return child;
                }
                var next = FindVisualChild(child, dataContext);
                if (next is not null)
                    return next;
            }
            return null!;
        }
    }

    /// <summary>
    /// 寻找视觉子元素
    /// </summary>
    /// <typeparam name="T">子元素类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <param name="outValue">子元素</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindVisualChild<T>(
        this DependencyObject obj,
        [MaybeNullWhen(false)] out T outValue
    )
        where T : DependencyObject
    {
        ArgumentNullException.ThrowIfNull(obj);

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
