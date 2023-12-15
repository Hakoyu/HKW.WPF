using HKW.HKWUtils.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF;

internal class MainWindowVM : ObservableClass<MainWindowVM>
{
    public ObservableCommand<string> FailedCheckAllCommand { get; } = new();

    public ObservableList<string> Datas { get; set; } = new() { "2", "3", "4" };

    public MainWindowVM()
    {
        FailedCheckAllCommand.ExecuteCommand += FailedCheckAll_ExecuteCommand;
    }

    private void FailedCheckAll_ExecuteCommand(string parameter)
    {
        return;
    }
}
