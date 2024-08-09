using System.ComponentModel;
using System.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using HKW.HKWReactiveUI;
using HKW.HKWUtils.Observable;
using HKW.WPF.Extensions;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 对话框窗口视图模型
/// </summary>
public partial class DialogWindowVM : ReactiveObjectX, IDialogViewModel, IModalDialogViewModel
{
    /// <inheritdoc/>
    public DialogWindowVM() { }

    /// <inheritdoc/>
    [ReactiveProperty]
    public string Title { get; set; } = string.Empty;

    /// <inheritdoc/>
    [ReactiveProperty]
    public bool? DialogResult { get; set; }

    /// <inheritdoc/>
    [ReactiveProperty]
    public bool IsThreeStateResult { get; set; }

    /// <inheritdoc/>
    [ReactiveProperty]
    public MessageBoxButton Button { get; set; } = MessageBoxButton.YesNo;

    #region Command

    #endregion
}
