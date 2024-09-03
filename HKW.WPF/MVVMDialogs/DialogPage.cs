using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 对话框页面
/// </summary>
public class DialogPage : Page, IDialogPage
{
    /// <inheritdoc/>
    public Window DialogWindow { get; set; } = null!;
}

/// <summary>
/// 对话框页面接口
/// </summary>
public interface IDialogPage
{
    /// <summary>
    /// 对话框窗口
    /// </summary>
    public Window DialogWindow { get; set; }
}
