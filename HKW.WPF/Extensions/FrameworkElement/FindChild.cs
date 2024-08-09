using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.Extensions;

/// <summary>
/// WPF拓展
/// </summary>
public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找子元素
    /// </summary>
    /// <typeparam name="T">子元素类型</typeparam>
    /// <param name="control">源控件</param>
    /// <returns>子元素</returns>
    public static T FindChild<T>(this ContentControl control)
        where T : FrameworkElement
    {
        ArgumentNullException.ThrowIfNull(control, nameof(control));
        FindChild(control.Content);
        return null!;

        static T FindChild(object obj)
        {
            if (obj is T t)
                return t;
            else if (obj is ContentControl control)
            {
                FindChild(control.Content);
            }
            else if (obj is Panel panel)
            {
                foreach (var c in panel.Children)
                    FindChild(c);
            }
            return null!;
        }
    }
}
