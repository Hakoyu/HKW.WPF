using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HKW.MVVMDialogs;

namespace HKW.WPF.MVVMDialogs.Windows;

/// <summary>
/// ItemSelectionPage.xaml 的交互逻辑
/// </summary>
public partial class ItemSelectionPage : UserControl, IDialogPage<Window>
{
    /// <summary>
    /// 视图模型
    /// </summary>
    public ItemSelectionVM ViewModel => (ItemSelectionVM)DataContext;

    /// <inheritdoc/>
    public Window DialogWindow { get; set; } = null!;

    /// <inheritdoc/>
    public ItemSelectionPage()
    {
        InitializeComponent();
        DataContextChanged += ItemSelectionPage_DataContextChanged;
    }

    private void ItemSelectionPage_DataContextChanged(
        object sender,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (ViewModel is null)
            return;

        if (ViewModel.SelectedItems != null)
        {
            ListBox_Main.SelectionMode = SelectionMode.Multiple;
        }
    }
}
