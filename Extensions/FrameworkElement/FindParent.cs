﻿using System.Diagnostics.CodeAnalysis;

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
    /// <param name="element">源控件</param>
    /// <returns>指定类型的父级</returns>
    public static T FindParent<T>(this FrameworkElement element)
        where T : FrameworkElement
    {
        var parent = element.Parent as FrameworkElement;
        while (parent is not null)
        {
            if (parent is T t)
                return t;
            parent = parent.Parent as FrameworkElement;
        }
        return null!;
    }

    /// <summary>
    /// 尝试寻找父级
    /// </summary>
    /// <typeparam name="T">父级类型</typeparam>
    /// <param name="element">源控件</param>
    /// <param name="parent">找到的父级</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryFindParent<T>(
        this FrameworkElement element,
        [MaybeNullWhen(false)] out T parent
    )
        where T : FrameworkElement
    {
        var result = element.FindParent<T>();
        if (result is not null)
        {
            parent = result;
            return true;
        }
        parent = null;
        return false;
    }
}