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
using System.Windows.Shapes;
using HKW.WPF.MVVMDialogs;
using Panuon.WPF.UI;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// TextInputWindow.xaml 的交互逻辑
/// </summary>
public partial class TextInputWindowX : WindowX
{
    /// <summary>
    /// 视图模型
    /// </summary>
    public TextInputVM ViewModel => (TextInputVM)DataContext;

    /// <inheritdoc/>
    public TextInputWindowX()
    {
        InitializeComponent();
        DataContextChanged += TextInputWindow_DataContextChanged;
    }

    private void TextInputWindow_DataContextChanged(
        object sender,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (ViewModel.Button is HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.Ok)
        {
            Button_Yes.Visibility = Visibility.Collapsed;
            Button_No.Visibility = Visibility.Collapsed;
            Button_Cancel.Visibility = Visibility.Collapsed;
        }
        else if (
            ViewModel.Button is HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.YesNo
        )
        {
            Grid_Button.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Auto);
            Button_No.SetValue(Grid.ColumnProperty, 2);
            Button_Cancel.Visibility = Visibility.Collapsed;
            Button_Ok.Visibility = Visibility.Collapsed;
        }
        else if (
            ViewModel.Button
            is HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.OkCancel
        )
        {
            Grid_Button.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Auto);
            Button_Yes.Visibility = Visibility.Collapsed;
            Button_No.Visibility = Visibility.Collapsed;
            Button_Ok.SetValue(Grid.ColumnProperty, 0);
            Button_Cancel.SetValue(Grid.ColumnProperty, 2);
        }
        else if (
            ViewModel.Button
            is HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.YesNoCancel
        )
        {
            Button_Ok.Visibility = Visibility.Collapsed;
            Button_Cancel.SetValue(Grid.ColumnProperty, 2);
        }

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

    private void Button_No_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.DialogResult = false;
        Close();
    }

    private void Button_Yes_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.DialogResult = true;
        Close();
    }

    private void Button_Cancel_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.DialogResult = null;
        Close();
    }

    private void Button_Ok_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.DialogResult = true;
        Close();
    }
}
