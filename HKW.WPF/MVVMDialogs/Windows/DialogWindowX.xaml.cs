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

namespace HKW.WPF.MVVMDialogs.Windows;

/// <summary>
/// DialogWindow.xaml 的交互逻辑
/// </summary>
public partial class DialogWindowX : WindowX
{
    /// <summary>
    /// 视图模型
    /// </summary>
    public DialogWindowVM ViewModel => (DialogWindowVM)DataContext;

    /// <inheritdoc/>
    public DialogWindowX()
    {
        InitializeComponent();
        DataContextChanged += DialogWindow_DataContextChanged;
        // Frame.Content是异步设置的,需要使用Navigated事件来获取设置完成的值
        Frame_Main.Navigated += Frame_Main_Navigated;
    }

    private void Frame_Main_Navigated(
        object sender,
        System.Windows.Navigation.NavigationEventArgs e
    )
    {
        if (Frame_Main.Content is Page page)
            page.DataContext = ViewModel;
    }

    private void DialogWindow_DataContextChanged(
        object sender,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (DataContext is null)
            return;

        ResizeMode = ViewModel.ResizeMode.ToWPFResizeMode();
        WindowXCaption.SetButtons(this, ViewModel.CaptionButtons.ToPUICaptionButtons());

        // 设置按钮
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

        // 设置默认按钮
        if (ViewModel.DefeatButton is DefeatMessageBoxButton.OkOrYes)
        {
            if (
                ViewModel.Button
                is HanumanInstitute.MvvmDialogs.FrameworkDialogs.MessageBoxButton.Ok
            )
                Button_Ok.Focus();
            else
                Button_Yes.Focus();
        }
        else if (ViewModel.DefeatButton is DefeatMessageBoxButton.Cancel)
        {
            Button_Cancel.Focus();
        }
        else if (ViewModel.DefeatButton is DefeatMessageBoxButton.No)
        {
            Button_No.Focus();
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
