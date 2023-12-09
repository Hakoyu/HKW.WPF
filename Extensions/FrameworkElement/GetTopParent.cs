using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 获取一个元素的顶级元素, 他们肯定是 <see cref="Window"/>, <see cref="Page"/>, <see cref="UserControl"/> 中的一个
    /// </summary>
    /// <param name="element">元素</param>
    public static FrameworkElement GetTopParent(this FrameworkElement element)
    {
        if (element is Window || element is Page || element is UserControl)
            return element;
        var parent = element.Parent as FrameworkElement;
        while (parent is not null)
        {
            if (parent.Parent is null)
                return parent;
            parent = parent.Parent as FrameworkElement;
        }
        return null!;
    }

    /// <summary>
    /// 获取一个元素的顶级元素, 他们肯定是 <see cref="Window"/>, <see cref="Page"/>, <see cref="UserControl"/> 中的一个
    /// </summary>
    /// <param name="element">元素</param>
    public static T GetTopElement<T>(this FrameworkElement element)
        where T : FrameworkElement
    {
        var type = typeof(T);
        if (type != typeof(Window) && type != typeof(Page) && type != typeof(UserControl))
            throw new Exception("T type must be Window, Page or UserControl");
        return (T)GetTopParent(element);
    }
}
