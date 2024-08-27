using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HanumanInstitute.MvvmDialogs;
using Panuon.WPF.UI;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// MVVM对话框扩展
/// </summary>
public static partial class HKWMVVMDialogExtensions
{
    /// <summary>
    /// 显示消息框
    /// </summary>
    /// <param name="dialogService">对话框服务器</param>
    /// <param name="ownerViewModel">所有者视图模型</param>
    /// <param name="text">消息</param>
    /// <param name="title">标题</param>
    /// <param name="button">按钮</param>
    /// <param name="icon">图标</param>
    /// <param name="defaultResult">默认结果</param>
    /// <returns>结果</returns>
    public static bool? ShowMessageBoxX(
        this IDialogService dialogService,
        INotifyPropertyChanged ownerViewModel,
        string text,
        string title = "",
        HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton button =
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.Ok,
        HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage icon =
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage.None,
        bool? defaultResult = null
    )
    {
        var owner = dialogService.DialogManager.FindViewByViewModel(ownerViewModel)!;
        return MessageBoxX
            .Show((Window)owner.RefObj, text, title, button.ToWPFButton(), icon.ToPUIIcon())
            .ToResult(defaultResult);
    }

#pragma warning disable IDE0060 // 删除未使用的参数
    /// <summary>
    /// 显示消息框
    /// </summary>
    /// <param name="dialogService">对话框服务器</param>
    /// <param name="text">消息</param>
    /// <param name="title">标题</param>
    /// <param name="button">按钮</param>
    /// <param name="icon">图标</param>
    /// <param name="defaultResult">默认结果</param>
    /// <returns>结果</returns>
    public static bool? ShowMessageBoxX(
        this IDialogService dialogService,
        string text,
        string title = "",
        HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton button =
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.Ok,
        HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage icon =
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage.None,
        bool? defaultResult = null
    )
    {
        return MessageBoxX
            .Show(text, title, button.ToWPFButton(), icon.ToPUIIcon())
            .ToResult(defaultResult);
    }
#pragma warning restore IDE0060 // 删除未使用的参数
}
