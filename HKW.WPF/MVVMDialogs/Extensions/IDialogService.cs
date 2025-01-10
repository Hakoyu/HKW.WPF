using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HanumanInstitute.MvvmDialogs;
using HKW.MVVMDialogs;
using Panuon.WPF.UI;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// MVVM对话框扩展
/// </summary>
public static partial class MVVMDialogExtensions
{
    /// <summary>
    /// 显示一个新视图模型的窗口并返回视图接口
    /// </summary>
    /// <param name="dialogService">对话框服务</param>
    /// <param name="viewModel">视图模型</param>
    /// <returns>视图接口</returns>
    public static IView Show(this IDialogService dialogService, INotifyPropertyChanged viewModel)
    {
        dialogService.Show(null, viewModel);
        if (dialogService.DialogManager is DialogManagerX managerX)
            return managerX.FindLastViewByViewModel(viewModel)!;
        else
            return dialogService.DialogManager.FindViewByViewModel(viewModel)!;
    }

    /// <summary>
    /// 异步显示对话框
    /// </summary>
    /// <typeparam name="TViewModel">对话框视图模型类型</typeparam>
    /// <param name="dialogService">对话框服务</param>
    /// <param name="ownerViewModel">所有者视图模型</param>
    /// <returns>对话框视图模型任务</returns>
    public static TViewModel ShowDialogX<TViewModel>(
        this IDialogService dialogService,
        INotifyPropertyChanged ownerViewModel
    )
        where TViewModel : DialogViewModel, new()
    {
        var vm = new TViewModel();
        dialogService.ShowDialog(ownerViewModel, vm);
        return vm;
    }

    /// <summary>
    /// 异步显示对话框
    /// </summary>
    /// <typeparam name="TViewModel">对话框视图模型类型</typeparam>
    /// <param name="dialogService">对话框服务</param>
    /// <param name="ownerViewModel">所有者视图模型</param>
    /// <param name="viewModel">对话框视图模型</param>
    /// <returns>对话框视图模型任务</returns>
    public static TViewModel ShowDialogX<TViewModel>(
        this IDialogService dialogService,
        INotifyPropertyChanged ownerViewModel,
        TViewModel viewModel
    )
        where TViewModel : DialogViewModel
    {
        dialogService.ShowDialog(ownerViewModel, viewModel);
        return viewModel;
    }

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
