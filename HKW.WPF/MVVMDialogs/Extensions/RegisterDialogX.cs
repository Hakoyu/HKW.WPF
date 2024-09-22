using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HanumanInstitute.MvvmDialogs.Wpf;
using HKW.MVVMDialogs;
using HKW.WPF.MVVMDialogs.Windows;

namespace HKW.WPF.MVVMDialogs;

public static partial class MVVMDialogExtensions
{
    /// <summary>
    /// 以默认形式注册全部对话框
    /// </summary>
    /// <param name="strongViewLocator">强视图定位器</param>
    public static void RegisterAllDialogX(this StrongViewLocatorX strongViewLocator)
    {
        strongViewLocator.RegisterTextInputDialog();
        strongViewLocator.RegisterItemSelectionDialog();
    }

    #region ItemSelectionDialog
    /// <summary>
    /// 注册项目选择对话框, 使用默认对话框
    /// </summary>
    /// <param name="strongViewLocator">强视图定位器</param>
    public static void RegisterItemSelectionDialog(this StrongViewLocatorX strongViewLocator)
    {
        strongViewLocator.RegisterDialogX<ItemSelectionVM, DialogWindowX, ItemSelectionPage>();
    }

    /// <summary>
    /// 注册项目选择对话框
    /// <para>可在创建窗口或页面时进行操作, 例如替换样式</para>
    /// </summary>
    /// <param name="strongViewLocator">强视图定位器</param>
    /// <param name="windowAction">窗口行动</param>
    /// <param name="pageAction">页面行动</param>
    public static void RegisterItemSelectionDialog(
        this StrongViewLocatorX strongViewLocator,
        Action<DialogWindowX>? windowAction,
        Action<ItemSelectionPage>? pageAction
    )
    {
        strongViewLocator.RegisterDialogX<ItemSelectionVM, DialogWindowX, ItemSelectionPage>(
            windowAction,
            pageAction
        );
    }

    /// <summary>
    /// 注册项目选择对话框
    /// </summary>
    /// <typeparam name="TItemSelectionWindow">项目选择对话框类型</typeparam>
    /// <param name="strongViewLocator">强视图定位器</param>
    public static void RegisterItemSelectionDialog<TItemSelectionWindow>(
        this StrongViewLocator strongViewLocator
    )
        where TItemSelectionWindow : Window, new()
    {
        strongViewLocator.Register<ItemSelectionVM, TItemSelectionWindow>();
    }
    #endregion

    #region TextInputDialog
    /// <summary>
    /// 注册文本输入对话框, 使用默认对话框
    /// </summary>
    /// <param name="strongViewLocator">强视图定位器</param>
    public static void RegisterTextInputDialog(this StrongViewLocatorX strongViewLocator)
    {
        strongViewLocator.RegisterDialogX<TextInputVM, DialogWindowX, TextInputPage>();
    }

    /// <summary>
    /// 注册文本输入对话框
    /// <para>可在创建窗口或页面时进行操作, 例如替换样式</para>
    /// </summary>
    /// <param name="strongViewLocator">强视图定位器</param>
    /// <param name="windowAction">窗口行动</param>
    /// <param name="pageAction">页面行动</param>
    public static void RegisterTextInputDialog(
        this StrongViewLocatorX strongViewLocator,
        Action<DialogWindowX>? windowAction,
        Action<TextInputPage>? pageAction
    )
    {
        strongViewLocator.RegisterDialogX<TextInputVM, DialogWindowX, TextInputPage>(
            windowAction,
            pageAction
        );
    }

    /// <summary>
    /// 注册文本输入对话框
    /// </summary>
    /// <typeparam name="TTextInputWindow">文本输入对话框类型</typeparam>
    /// <param name="strongViewLocator">强视图定位器</param>
    public static void RegisterTextInputDialog<TTextInputWindow>(
        this StrongViewLocator strongViewLocator
    )
        where TTextInputWindow : Window, new()
    {
        strongViewLocator.Register<TextInputVM, TTextInputWindow>();
    }
    #endregion
}
