namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 单例对话框
/// </summary>
public interface IInstanceDialog
{
    /// <summary>
    /// 单例对话框结果
    /// </summary>
    public bool? InstanceDialogResult { get; set; }
}
