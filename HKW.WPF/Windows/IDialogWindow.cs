using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HKW.WPF.Windows;

/// <summary>
/// 对话框窗口接口
/// </summary>
public interface IDialogWindow
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
    /// 是三种状态的结果
    /// </summary>
    public bool IsThreeStateResult { get; set; }

    ///// <summary>
    ///// 显示
    ///// </summary>
    //public void Show();

    ///// <summary>
    ///// 显示
    ///// </summary>
    ///// <param name="owner">所有者</param>
    //public void Show(Window owner);

    /// <summary>
    /// 显示对话框
    /// </summary>
    public void ShowDialog();

    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="owner">所有者</param>
    public void ShowDialog(Window owner);

    /// <summary>
    /// 窗口关闭前
    /// </summary>
    public event CancelEventHandler Closing;

    /// <summary>
    /// 窗口关闭后
    /// </summary>
    public event EventHandler Closed;
}
