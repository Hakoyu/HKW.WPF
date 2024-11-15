using System.Windows;
using System.Windows.Controls;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 页面定位器
/// <para>用于窗口或控件显示单个页面时,视图定位器查找页面的当前窗口时使用</para>
/// <para>为工程中的所有窗口实现此接口有助于提高查询性能</para>
/// </summary>
public interface IPageLocator
{
    /// <summary>
    /// 基于页面类型的页面定位器
    /// <para>(PageType, GetPage)</para>
    /// <para>设置为 <see langword="null"/> 时即表面自身没有可查询页面,会直接跳过</para>
    /// </summary>
    public Dictionary<Type, Func<Window, UserControl?>>? PageLocatorByType { get; }
}
