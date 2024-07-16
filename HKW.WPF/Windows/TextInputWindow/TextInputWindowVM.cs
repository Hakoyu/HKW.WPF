using System.Diagnostics;
using System.Windows;
using HKW.HKWReactiveUI;
using HKW.HKWUtils.Observable;

namespace HKW.WPF.Windows;

/// <summary>
/// 文本输入框视图模型
/// </summary>
public class TextInputWindowVM : DialogWindowVM
{
    /// <inheritdoc/>
    public TextInputWindowVM(Window window)
        : base(window) { }

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

    #region Command

    #endregion
}
