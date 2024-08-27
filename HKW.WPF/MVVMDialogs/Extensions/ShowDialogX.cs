using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs;

namespace HKW.WPF.MVVMDialogs;

public static partial class HKWMVVMDialogExtensions
{
    /// <summary>
    /// 显示文本输入对话框
    /// </summary>
    /// <param name="dialogService">对话框服务</param>
    /// <param name="ownerViewModel">所有者视图模型</param>
    /// <param name="textInputVM">文本输入对话框视图模型</param>
    /// <returns>结果</returns>
    public static TextInputVM ShowTextInputDialog(
        this IDialogService dialogService,
        INotifyPropertyChanged ownerViewModel,
        TextInputVM textInputVM
    )
    {
        dialogService.ShowDialog(ownerViewModel, textInputVM);
        return textInputVM;
    }

    /// <summary>
    /// 显示文本输入对话框
    /// </summary>
    /// <param name="dialogService">对话框服务</param>
    /// <param name="ownerViewModel">所有者视图模型</param>
    /// <param name="itemSelectionVM">项目选择对话框视图模型</param>
    /// <returns>结果</returns>
    public static ItemSelectionVM ShowItemSelectionDialog(
        this IDialogService dialogService,
        INotifyPropertyChanged ownerViewModel,
        ItemSelectionVM itemSelectionVM
    )
    {
        dialogService.ShowDialog(ownerViewModel, itemSelectionVM);
        return itemSelectionVM;
    }
}
