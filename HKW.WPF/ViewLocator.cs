using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs.Wpf;

namespace HKW.WPF;

/// <summary>
/// 视图定位器
/// </summary>
internal class ViewLocator : StrongViewLocator
{
    /// <inheritdoc/>
    public ViewLocator()
    {
        Register<MainWindowVM, MainWindow>();
        //this.RegisterAllDialogX();
    }
}
