using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;
using Panuon.WPF.UI;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// MVVM对话框扩展
/// </summary>
public static partial class HKWMVVMDialogExtensions
{
    /// <summary>
    /// 转换为结果
    /// </summary>
    /// <param name="result">提示框结果</param>
    /// <param name="defaultResult">默认结果</param>
    /// <returns>结果</returns>
    public static bool? ToResult(this MessageBoxResult result, bool? defaultResult = null)
    {
        return result switch
        {
            MessageBoxResult.OK => true,
            MessageBoxResult.Yes => true,
            MessageBoxResult.No => false,
            MessageBoxResult.Cancel => null,
            _ => defaultResult
        };
    }

    /// <summary>
    /// 转换为PUI图标
    /// </summary>
    /// <param name="icon">图标</param>
    /// <returns>PUI图标</returns>
    public static MessageBoxIcon ToPUIIcon(
        this HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage icon
    )
    {
        return icon switch
        {
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage.None
                => MessageBoxIcon.None,
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage.Information
                => MessageBoxIcon.Info,
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage.Warning
                => MessageBoxIcon.Warning,
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxImage.Error
                => MessageBoxIcon.Error,
            _ => MessageBoxIcon.None
        };
    }

    /// <summary>
    /// 转换为WPF按钮
    /// </summary>
    /// <param name="button">MVVM窗口按钮</param>
    /// <returns>WPF按钮</returns>
    public static MessageBoxButton ToFrameworkButton(
        this HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton button
    )
    {
        return button switch
        {
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.Ok
                => MessageBoxButton.OK,
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.OkCancel
                => MessageBoxButton.OKCancel,
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.YesNo
                => MessageBoxButton.YesNo,
            HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.YesNoCancel
                => MessageBoxButton.YesNoCancel,
            _ => MessageBoxButton.OK
        };
    }
}
