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

    public ObservableList<TestVM> Datas { get; set; } =
        new(Enumerable.Range(0, 100).Select(i => new TestVM() { Content = i.ToString() }));

    public MainWindowVM()
    {
        FailedCheckAllCommand.ExecuteCommand += FailedCheckAll_ExecuteCommand;
        foreach (var vm in Datas)
            vm.PropertyChangedX += Vm_PropertyChangedX;
    }

    int _changeCount = 0;

    private void Vm_PropertyChangedX(TestVM sender, PropertyChangedXEventArgs e)
    {
        _changeCount++;
        return;
    }

    private void FailedCheckAll_ExecuteCommand(string parameter)
    {
        return;
    }
}

internal class TestVM : ObservableClass<TestVM>
{
    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set => SetProperty(ref _isChecked, value);
    }

    private string _content = string.Empty;
    public string Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
}
