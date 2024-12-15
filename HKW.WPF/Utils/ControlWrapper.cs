using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HKW.HKWReactiveUI;

namespace HKW.WPF.Utils;

/// <summary>
/// 控件包装器
/// </summary>
/// <typeparam name="TControl">控件类型</typeparam>
public partial class ControlWrapper<TControl> : ReactiveObjectX
    where TControl : FrameworkElement
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="createControl">创建控件</param>
    public ControlWrapper(Func<ControlWrapper<TControl>, TControl> createControl)
    {
        CreateControlAction = createControl;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TControl? _control;

    /// <summary>
    /// 控件
    /// </summary>
    public TControl Control => _control ??= CreateControlAction(this);

    /// <summary>
    /// 创建控件
    /// </summary>
    public Func<ControlWrapper<TControl>, TControl> CreateControlAction { get; private set; }

    /// <summary>
    /// 显示文本行动
    /// </summary>
    public Func<ControlWrapper<TControl>, string>? DisplayTextAction { get; set; }

    /// <summary>
    /// 显示文本
    /// </summary>
    [ReactiveProperty]
    public string DisplayText { get; set; } = string.Empty;

    /// <summary>
    /// 创建控件
    /// </summary>
    public TControl CreateControl()
    {
        return _control = CreateControlAction(this);
    }

    /// <summary>
    /// 刷新显示文本
    /// </summary>
    public void RefreshDisplayText()
    {
        DisplayText = DisplayTextAction?.Invoke(this) ?? string.Empty;
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        base.Dispose(disposing);

        if (_control is not null)
        {
            if (_control.DataContext is IDisposable disposable)
                disposable.Dispose();
            _control.DataContext = null;
            _control = null;
        }
    }
}
