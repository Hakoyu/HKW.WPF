using HanumanInstitute.MvvmDialogs.FrameworkDialogs;

namespace HKW.WPF.MVVMDialogs;

/// <inheritdoc cref="SaveFileDialogSettings"/>
public class SaveFileDialogSettingsX : SaveFileDialogSettings
{
    /// <inheritdoc/>
    public SaveFileDialogSettingsX() { }

    /// <inheritdoc/>
    /// <param name="fileFilter1">文件过滤器1</param>
    public SaveFileDialogSettingsX(FileFilter fileFilter1)
    {
        Filters.Add(fileFilter1);
    }

    /// <inheritdoc/>
    /// <param name="fileFilter1">文件过滤器1</param>
    /// <param name="fileFilter2">文件过滤器2</param>
    public SaveFileDialogSettingsX(FileFilter fileFilter1, FileFilter fileFilter2)
    {
        Filters.Add(fileFilter1);
        Filters.Add(fileFilter2);
    }

    /// <inheritdoc/>
    /// <param name="fileFilter1">文件过滤器1</param>
    /// <param name="fileFilter2">文件过滤器2</param>
    /// <param name="fileFilter3">文件过滤器3</param>
    public SaveFileDialogSettingsX(
        FileFilter fileFilter1,
        FileFilter fileFilter2,
        FileFilter fileFilter3
    )
    {
        Filters.Add(fileFilter1);
        Filters.Add(fileFilter2);
        Filters.Add(fileFilter3);
    }

    /// <inheritdoc/>
    /// <param name="fileFilter1">文件过滤器1</param>
    /// <param name="fileFilter2">文件过滤器2</param>
    /// <param name="fileFilter3">文件过滤器3</param>
    /// <param name="fileFilter4">文件过滤器4</param>
    public SaveFileDialogSettingsX(
        FileFilter fileFilter1,
        FileFilter fileFilter2,
        FileFilter fileFilter3,
        FileFilter fileFilter4
    )
    {
        Filters.Add(fileFilter1);
        Filters.Add(fileFilter2);
        Filters.Add(fileFilter3);
        Filters.Add(fileFilter4);
    }
}
