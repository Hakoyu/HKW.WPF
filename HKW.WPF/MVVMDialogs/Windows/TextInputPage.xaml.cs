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
/// TextInputPage.xaml 的交互逻辑
/// </summary>
public partial class TextInputPage : UserControl, IDialogPage<Window>
{
    /// <summary>
    /// 视图模型
    /// </summary>
    public TextInputVM ViewModel => (TextInputVM)DataContext;

    /// <inheritdoc/>
    public Window DialogWindow { get; set; } = null!;

    /// <inheritdoc/>
    public TextInputPage()
    {
        InitializeComponent();
        DataContextChanged += TextInputPage_DataContextChanged;
    }

    private void TextInputPage_DataContextChanged(
        object sender,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (ViewModel.MultiLineMode)
        {
            TextBox_Text.Height = double.NaN;
            TextBox_Text.TextWrapping = TextWrapping.Wrap;
            TextBox_Text.VerticalContentAlignment = VerticalAlignment.Top;
            TextBox_Text.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            TextBox_Text.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            TextBox_Text.AcceptsReturn = true;
        }
    }
}
