using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HKW.WPF;

/// <summary>
/// 图片工具
/// </summary>
public static class HKWImageUtils
{
    ///// <summary>
    ///// 载入位图
    ///// </summary>
    ///// <param name="file">文件</param>
    ///// <returns>位图</returns>
    //public static Bitmap LoadBitmap(string file)
    //{
    //    if (string.IsNullOrWhiteSpace(file) || File.Exists(file) is false)
    //        return null!;
    //    return new(file);
    //}

    /// <summary>
    /// 载入图片
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage LoadImage(string? file, bool endInit = true)
    {
        if (string.IsNullOrWhiteSpace(file) || File.Exists(file) is false)
            return null!;
        BitmapImage image = new();
        image.BeginInit();
        try
        {
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = new FileStream(
                file,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite
            );
        }
        catch
        {
            image.StreamSource?.Close();
            image.StreamSource = null;
        }
        finally
        {
            if (endInit)
            {
                image.EndInit();
                image.Freeze();
            }
        }
        return image;
    }

    /// <summary>
    /// 载入图片
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage LoadImage(Stream stream, bool endInit = true)
    {
        BitmapImage image = new();
        image.BeginInit();
        try
        {
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
        }
        catch
        {
            image.StreamSource?.Close();
            image.StreamSource = null;
        }
        finally
        {
            if (endInit)
            {
                image.EndInit();
                image.Freeze();
            }
        }
        return image;
    }

    /// <summary>
    /// 载入图片至内存流
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage LoadImageToMemory(string file, bool endInit = true)
    {
        BitmapImage image = new();
        image.BeginInit();
        try
        {
            image.CacheOption = BitmapCacheOption.OnLoad;
            using var fs = new FileStream(
                file,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite
            );
            var bytes = new byte[fs.Length];
            fs.Read(bytes);
            image.StreamSource = new MemoryStream(bytes);
        }
        catch
        {
            image.StreamSource?.Close();
            image.StreamSource = null;
        }
        finally
        {
            if (endInit)
            {
                image.EndInit();
                image.Freeze();
            }
        }
        return image;
    }
}
