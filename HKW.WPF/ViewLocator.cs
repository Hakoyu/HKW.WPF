using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs.Wpf;
using HKW.WPF.MVVMDialogs;

namespace HKW.WPF;

/// <summary>
/// 视图定位器
/// </summary>
internal class ViewLocator : StrongViewLocatorX
{
    /// <inheritdoc/>
    public ViewLocator()
    {
        Register<MainWindowVM, MainWindow>();
        this.RegisterTextInputDialog();
    }
}
