using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HKW.WPF.Windows;

/// <summary>
/// 对话框窗口
/// </summary>
public static class DialogWindow
{
    /// <summary>
    /// 创建文本输入窗口
    /// </summary>
    /// <returns></returns>
    public static TextInputWindowVM CreateTextInputWindow<T>()
        where T : Window, new()
    {
        return new TextInputWindowVM(new T());
    }

    /// <summary>
    /// 创建文本输入窗口
    /// </summary>
    /// <returns></returns>
    public static TextInputWindowVM CreateTextInputWindow<T>(T window)
        where T : Window
    {
        return new TextInputWindowVM(window);
    }
}
