using System.ComponentModel;
using System.Windows.Controls;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 强视图定位器
/// </summary>
public class StrongViewLocatorX : StrongViewLocator
{
    /// <summary>
    /// 注册页面
    /// </summary>
    /// <typeparam name="TViewModel">视图模型类型</typeparam>
    /// <typeparam name="TPage">页面类型</typeparam>
    public void RegisterPage<TViewModel, TPage>()
        where TViewModel : INotifyPropertyChanged
        where TPage : Page, new()
    {
        Register<TViewModel>(new ViewDefinition(typeof(TPage), () => new TPage()));
    }
}
