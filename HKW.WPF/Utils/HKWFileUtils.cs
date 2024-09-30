using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;

namespace HKW.WPF;

/// <summary>
/// 文件工具
/// </summary>
public static class HKWFileUtils
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    private struct SHFILEOPSTRUCT
    {
        public int hwnd;

        [MarshalAs(UnmanagedType.U4)]
        public int wFunc;
        public string pFrom;
        public string pTo;
        public short fFlags;

        [MarshalAs(UnmanagedType.Bool)]
        public bool fAnyOperationsAborted;
        public int hNameMappings;
        public string lpszProgressTitle;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

    private const int FO_DELETE = 3;
    private const int FOF_ALLOWUNDO = 0x40;
    private const int FOF_NOCONFIRMATION = 0x10;

    /// <summary>
    /// 删除文件至回收站
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="showConfirmDialog">显示文件窗口</param>
    /// <returns>返回码</returns>
    public static int DeleteFileToRecyclebin(string file, bool showConfirmDialog = false)
    {
        var shf = new SHFILEOPSTRUCT
        {
            wFunc = FO_DELETE,

            fFlags = FOF_ALLOWUNDO
        };

        if (!showConfirmDialog)
        {
            shf.fFlags |= FOF_NOCONFIRMATION;
        }

        shf.pFrom = $"{file}\0\0";

        return SHFileOperation(ref shf);
    }
}
