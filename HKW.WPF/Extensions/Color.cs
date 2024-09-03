using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 是亮色
    /// </summary>
    /// <param name="color">颜色</param>
    /// <returns>是为 <see langword="true"/> 不是为 <see langword="false"/></returns>
    public static bool IsLightColor(this System.Windows.Media.Color color)
    {
        double darkness = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
        return darkness < 0.5;
    }

    /// <summary>
    /// 是亮色
    /// </summary>
    /// <param name="color">颜色</param>
    /// <returns>是为 <see langword="true"/> 不是为 <see langword="false"/></returns>
    public static bool IsLightColor(System.Drawing.Color color)
    {
        double darkness = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
        return darkness < 0.5;
    }
}
