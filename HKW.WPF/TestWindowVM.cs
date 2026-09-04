using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs;
using ReactiveUI;

namespace HKW.WPF;

internal partial class TestWindowVM : ReactiveObject, IModalDialogViewModel
{
    public bool? DialogResult { get; set; }
}
