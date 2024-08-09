using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;

namespace HKW.WPF;

/// <summary>
/// 对话框窗口接口
/// </summary>
public interface IDialogViewModel
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 对话框结果
    /// </summary>
    public bool? DialogResult { get; }

    /// <summary>
    /// 按钮
    /// </summary>
    public MessageBoxButton Button { get; set; }
}
