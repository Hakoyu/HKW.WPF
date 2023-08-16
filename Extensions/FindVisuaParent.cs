using System.Windows.Media;

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
    /// <param name="reference">源控件</param>
    /// <returns></returns>
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
}
