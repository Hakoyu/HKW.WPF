using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 寻找视觉子项
    /// </summary>
    /// <typeparam name="T">子项类型</typeparam>
    /// <param name="obj">源控件</param>
    /// <param name="outValue">子项</param>
    /// <returns>找到为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public static bool TryFindVisualChild<T>(
        this DependencyObject obj,
        [NotNullWhen(true)] out T? outValue
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
