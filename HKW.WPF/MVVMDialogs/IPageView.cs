using System.Windows.Controls;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 页面定位器
/// <para>用于窗口或控件显示单个页面时,视图定位器查找页面的当前窗口时使用</para>
/// </summary>
public interface IPageView
{
    /// <summary>
    /// 当前页面
    /// </summary>
    public Page? CurrentPage { get; set; }
}
