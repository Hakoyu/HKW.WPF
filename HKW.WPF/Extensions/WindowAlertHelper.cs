using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace HKW.WPF.Extensions;

/// <summary>
/// 窗口警报助手
/// </summary>
public static class WindowAlertHelper
{
    /// <summary>
    /// 触发对话框警告（闪烁 + 叮声）
    /// </summary>
    /// <param name="window">目标WPF窗口</param>
    /// <param name="flashCount">闪烁次数</param>
    public static void PlayAlert(this Window window, uint flashCount = 3)
    {
        if (window == null)
            return;

        // 获取句柄
        IntPtr hwnd = new WindowInteropHelper(window).Handle;
        if (hwnd == IntPtr.Zero)
            return;

        // 1. 播放系统警告“叮”声
        PInvoke.MessageBeep(MESSAGEBOX_STYLE.MB_ICONEXCLAMATION);

        // 2. 配置闪烁信息
        FLASHWINFO fInfo = new FLASHWINFO
        {
            cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO))),
            hwnd = (HWND)hwnd,
            dwTimeout = 0, // 使用系统默认闪烁频率
        };

        fInfo.dwFlags = FLASHWINFO_FLAGS.FLASHW_ALL; // 闪烁指定次数
        fInfo.uCount = flashCount;

        // 3. 执行闪烁
        PInvoke.FlashWindowEx(in fInfo);
    }

    /// <summary>
    /// 将窗口移动到当前所在显示器的正中央（支持多显示器和不同的DPI）
    /// </summary>
    /// <param name="window">目标WPF窗口</param>
    /// <param name="useWorkArea">是否避开任务栏（true表示在除任务栏外的中央，false表示整个屏幕的中央）</param>
    public static void CenterOnScreen(this Window window, bool useWorkArea = true)
    {
        if (window == null)
            return;

        // 获取窗口句柄
        var hwnd = (HWND)new WindowInteropHelper(window).Handle;
        if (hwnd == HWND.Null)
            return;

        // 1. 获取当前窗口所在的显示器信息
        var hMonitor = PInvoke.MonitorFromWindow(hwnd, MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONEAREST);
        var monitorInfo = new MONITORINFO();
        monitorInfo.cbSize = (uint)Marshal.SizeOf(monitorInfo);

        if (!PInvoke.GetMonitorInfo(hMonitor, ref monitorInfo))
            return;

        // 选择使用工作区还是整个屏幕绝对坐标
        RECT targetScreen = useWorkArea ? monitorInfo.rcWork : monitorInfo.rcMonitor;

        // 2. 获取当前窗口的实际物理像素大小（避免WPF的DPI缩放干扰）
        if (!PInvoke.GetWindowRect(hwnd, out RECT windowRect))
            return;
        int windowWidth = windowRect.right - windowRect.left;
        int windowHeight = windowRect.bottom - windowRect.top;

        // 3. 计算居中坐标
        int screenWidth = targetScreen.right - targetScreen.left;
        int screenHeight = targetScreen.bottom - targetScreen.top;

        int newX = targetScreen.left + (screenWidth - windowWidth) / 2;
        int newY = targetScreen.top + (screenHeight - windowHeight) / 2;

        // 4. 移动窗口
        PInvoke.MoveWindow(hwnd, newX, newY, windowWidth, windowHeight, true);
    }

    /// <summary>
    /// 恢复最小化的窗口到之前状态(可能是普通或最大化)
    /// </summary>
    public static void RestoredState(this Window window)
    {
        if (window.WindowState != WindowState.Minimized)
            return;

        var hwnd = (HWND)new WindowInteropHelper(window).Handle;
        if (hwnd == HWND.Null)
            return;

        // 读取 Windows 记录的窗口放置信息
        var placement = new WINDOWPLACEMENT();
        placement.length = (uint)Marshal.SizeOf(placement);

        if (PInvoke.GetWindowPlacement(hwnd, ref placement))
        {
            // 如果最小化前的状态是最大化(SW_SHOWMAXIMIZED)，就恢复为最大化
            if (
                placement.showCmd == SHOW_WINDOW_CMD.SW_SHOWMAXIMIZED
                || (placement.flags & WINDOWPLACEMENT_FLAGS.WPF_RESTORETOMAXIMIZED) != 0
            )
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }
        else
        {
            window.WindowState = WindowState.Normal;
        }

        // 强制刷新布局
        window.UpdateLayout();
    }
}
