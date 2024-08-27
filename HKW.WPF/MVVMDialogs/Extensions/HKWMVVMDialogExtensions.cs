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
    public static MessageBoxButton ToWPFButton(
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

    /// <summary>
    /// 转换为WPF调整模式
    /// </summary>
    /// <param name="resizeMode">调整模式</param>
    /// <returns>WPF调整模式</returns>
    public static System.Windows.ResizeMode ToWPFResizeMode(this ResizeMode resizeMode)
    {
        return resizeMode switch
        {
            ResizeMode.NoResize => System.Windows.ResizeMode.NoResize,
            ResizeMode.CanMinimize => System.Windows.ResizeMode.CanMinimize,
            ResizeMode.CanResize => System.Windows.ResizeMode.CanResize,
            ResizeMode.CanResizeWithGrip => System.Windows.ResizeMode.CanResizeWithGrip,
            _ => System.Windows.ResizeMode.NoResize,
        };
    }

    /// <summary>
    /// 转换为PUI标题按钮
    /// </summary>
    /// <param name="captionButtons">标题按钮</param>
    /// <returns>PUI标题按钮</returns>
    public static Panuon.WPF.UI.CaptionButtons ToPUICaptionButtons(
        this CaptionButtons captionButtons
    )
    {
        return captionButtons switch
        {
            CaptionButtons.All => Panuon.WPF.UI.CaptionButtons.All,
            CaptionButtons.Close => Panuon.WPF.UI.CaptionButtons.Close,
            CaptionButtons.MinimizeClose => Panuon.WPF.UI.CaptionButtons.MinimizeClose,
            CaptionButtons.MaximizeClose => Panuon.WPF.UI.CaptionButtons.MaximizeClose,
            CaptionButtons.None => Panuon.WPF.UI.CaptionButtons.None,
            _ => Panuon.WPF.UI.CaptionButtons.All
        };
    }
}
