using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using HanumanInstitute.MvvmDialogs;
using HKW.HKWReactiveUI;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 文本输入框视图模型
/// </summary>
public partial class TextInputVM : DialogWindowVM
{
    /// <inheritdoc/>
    public TextInputVM() { }

    /// <summary>
    /// 文本
    /// </summary>
    [ReactiveProperty]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 水印
    /// </summary>
    [ReactiveProperty]
    public string Watermark { get; set; } = string.Empty;

    /// <summary>
    /// 多行模式, 文本框可换行
    /// </summary>
    [ReactiveProperty]
    public bool MultiLineMode { get; set; }

    #region Command

    #endregion
}
