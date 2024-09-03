using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using HKW.WPF.Extensions;
using HKW.WPF.Native;

namespace HKW.WPF;

/// <summary>
/// 系统参数
/// </summary>
public class SystemParametersX
{
    /// <inheritdoc/>
    static SystemParametersX()
    {
        if (PInvoke.GetSystemAccentColor() is Color accentColor)
        {
            SystemAccentColor = accentColor;
            SystemAccentBrush = new SolidColorBrush(SystemAccentColor);
        }
        else
        {
            // SystemParameters.WindowGlassColor 比实际主题色稍暗
            // 使用增强亮度的方式进行修改, 但无法保证修改后的颜色能与主题色一致
            var color = AdjustBrightness(SystemParameters.WindowGlassColor, 1.08);
            SystemAccentColor = color;
            SystemAccentBrush = new SolidColorBrush(color);
        }
        SystemAccentForegroundColor = SystemAccentColor.IsLightColor()
            ? Colors.Black
            : Colors.White;
        SystemAccentForegroundBrush = new SolidColorBrush(SystemAccentForegroundColor);
    }

    /// <summary>
    /// 系统主题色
    /// </summary>
    public static Color SystemAccentColor { get; }

    /// <summary>
    /// 系统主题色笔刷
    /// </summary>
    public static Brush SystemAccentBrush { get; }

    /// <summary>
    /// 系统主题前景色
    /// </summary>
    public static Color SystemAccentForegroundColor { get; }

    /// <summary>
    /// 系统主题前景色
    /// </summary>
    public static Brush SystemAccentForegroundBrush { get; }

    private static Color AdjustBrightness(Color color, double factor)
    {
        return Color.FromArgb(
            color.A,
            (byte)Math.Min(255, color.R * factor),
            (byte)Math.Min(255, color.G * factor),
            (byte)Math.Min(255, color.B * factor)
        );
    }
}
