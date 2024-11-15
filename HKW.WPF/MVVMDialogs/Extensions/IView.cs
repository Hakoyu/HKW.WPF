using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HanumanInstitute.MvvmDialogs;
using HKW.WPF.Extensions;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// MVVM对话框扩展
/// </summary>
public static partial class MVVMDialogExtensions
{
    /// <summary>
    /// 隐藏
    /// </summary>
    /// <param name="view">视图</param>
    public static void Hide(this IView view)
    {
        if (view.RefObj is Window window)
            window.Hide();
    }

    /// <summary>
    /// 显示或聚焦
    /// </summary>
    /// <param name="view">视图</param>
    public static void ShowOrActivate(this IView view)
    {
        if (view.RefObj is Window window)
            window.ShowOrActivate();
    }
}
