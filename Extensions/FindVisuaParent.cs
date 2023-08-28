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
}
