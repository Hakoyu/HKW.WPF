using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HanumanInstitute.MvvmDialogs.Wpf;

namespace HKW.WPF.MVVMDialogs;

public static partial class HKWMVVMDialogExtensions
{
    /// <summary>
    /// 注册文本输入对话框, 使用默认对话框
    /// </summary>
    /// <param name="strongViewLocator">强视图定位器</param>
    public static void RegisterTextInputDialog(this StrongViewLocator strongViewLocator)
    {
        strongViewLocator.Register<TextInputVM, TextInputWindowX>();
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
}
