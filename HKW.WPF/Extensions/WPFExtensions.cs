using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 转换为绘画点
    /// </summary>
    /// <param name="point">点</param>
    /// <returns>绘画点</returns>
    public static System.Drawing.Point ToDrawingPoint(this System.Windows.Point point)
    {
        return new System.Drawing.Point((int)Math.Round(point.X), (int)Math.Round(point.Y));
    }
}
