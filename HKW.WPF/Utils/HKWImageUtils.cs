using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HKW.WPF.Utils;

/// <summary>
/// 图片工具
/// </summary>
public static class HKWImageUtils
{
    /// <summary>
    /// 创建图片
    /// </summary>
    /// <param name="imagePath">图片路径</param>
    /// <param name="beginInitAction">初始化行动</param>
    /// <returns>图片</returns>
    public static BitmapImage LoadImage(
        string? imagePath,
        Action<BitmapImage>? beginInitAction = null
    )
    {
        if (string.IsNullOrWhiteSpace(imagePath) || File.Exists(imagePath) is false)
            return null!;
        BitmapImage image = new();
        image.BeginInit();
        try
        {
            image.StreamSource = new FileStream(
                imagePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite
            );
            beginInitAction?.Invoke(image);
        }
        catch
        {
            image.StreamSource?.Close();
            image.StreamSource = null;
        }
        finally
        {
            image.EndInit();
            image.Freeze();
        }
        return image;
    }

    /// <summary>
    /// 创建图片
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="beginInitAction">初始化行动</param>
    /// <returns>图片</returns>
    public static BitmapImage LoadImage(Stream stream, Action<BitmapImage>? beginInitAction = null)
    {
        BitmapImage image = new();
        image.BeginInit();
        try
        {
            image.StreamSource = stream;
            beginInitAction?.Invoke(image);
        }
        catch
        {
            image.StreamSource?.Close();
            image.StreamSource = null;
        }
        finally
        {
            image.EndInit();
            image.Freeze();
        }
        return image;
    }

    /// <summary>
    /// 载入图片至内存流
    /// </summary>
    /// <param name="imagePath">图片路径</param>
    /// <param name="beginInitAction">初始化行动</param>
    /// <returns>图片</returns>
    public static BitmapImage LoadImageToMemory(
        string imagePath,
        Action<BitmapImage>? beginInitAction = null
    )
    {
        BitmapImage image = new();
        image.BeginInit();
        try
        {
            using var fs = new FileStream(
                imagePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite
            );
            var bytes = new byte[fs.Length];
            fs.Read(bytes);
            image.StreamSource = new MemoryStream(bytes);
            beginInitAction?.Invoke(image);
        }
        catch
        {
            image.StreamSource?.Close();
            image.StreamSource = null;
        }
        finally
        {
            image.EndInit();
            image.Freeze();
        }
        return image;
    }
}
