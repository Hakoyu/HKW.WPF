using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 执行事件
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="routedEvent">路由事件</param>
    public static void RaiseEvent(this IInputElement element, RoutedEvent routedEvent)
    {
        element.RaiseEvent(new(routedEvent));
    }
}
