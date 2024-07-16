using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using HKW.HKWReactiveUI;
using HKW.HKWUtils.Observable;
using HKW.WPF.Extensions;

namespace HKW.WPF.Windows;

/// <summary>
/// 对话框窗口视图模型
/// </summary>
public partial class DialogWindowVM : ReactiveObjectX, IDialogWindow
{
    private readonly Window _window;

    /// <inheritdoc/>
    public DialogWindowVM(Window window)
    {
        _window = window;
        _window.SetViewModel(this);
        Closing -= DialogWindowVM_Closing;
        Closed -= DialogWindowVM_Closed;
        Closing += DialogWindowVM_Closing;
        Closed += DialogWindowVM_Closed;
    }

    private void DialogWindowVM_Closing(object? sender, CancelEventArgs e)
    {
        if (sender is not Window window)
            return;
        DialogResult = window.DialogResult;
    }

    private void DialogWindowVM_Closed(object? sender, EventArgs e)
    {
        Closing -= DialogWindowVM_Closing;
        Closed -= DialogWindowVM_Closed;
    }

    /// <inheritdoc/>
    [ReactiveProperty]
    public string Title { get; set; } = string.Empty;

    /// <inheritdoc/>
    [ReactiveProperty]
    public bool? DialogResult { get; set; }

    /// <inheritdoc/>
    [ReactiveProperty]
    public bool IsThreeStateResult { get; set; }

    #region Command

    #endregion

    /// <inheritdoc/>
    public void ShowDialog() => _window.ShowDialog();

    /// <inheritdoc/>
    public void ShowDialog(Window owner)
    {
        _window.Owner = owner;
        _window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        _window.ShowDialog();
    }

    /// <inheritdoc/>
    public event EventHandler? Closed
    {
        add => _window.Closed += value;
        remove => _window.Closed -= value;
    }

    /// <inheritdoc/>
    public event CancelEventHandler? Closing
    {
        add => _window.Closing += value;
        remove => _window.Closing -= value;
    }
}
