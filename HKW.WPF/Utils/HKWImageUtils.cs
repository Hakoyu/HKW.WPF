using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using HKW.HKWUtils.Collections;
using Splat;

namespace HKW.WPF;

/// <summary>
/// 图片工具
/// </summary>
public static class HKWImageUtils
{
    /// <summary>
    /// 所有外部图像
    /// </summary>
    public static BidirectionalDictionary<string, BitmapImage> ImageByPath { get; } = new([], []);

    /// <summary>
    /// 所有图像
    /// </summary>
    public static HashSet<BitmapImage> Images { get; } = [];

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
    /// <param name="logger">日志记录器</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage? LoadImage(
        string? file,
        IEnableLogger? logger = null,
        bool endInit = true
    )
    {
        if (File.Exists(file) is false)
            return null;
        if (ImageByPath.TryGetValue(file, out var oldImage))
            return oldImage;
        var image = new BitmapImage();
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
        catch (Exception ex)
        {
            logger?.Log().Warn(ex);
            image.StreamSource?.Close();
            image.StreamSource = null;
            image.EndInit();
            image.Freeze();
            image = null;
        }
        finally
        {
            if (endInit)
            {
                image?.EndInit();
                image?.Freeze();
            }
        }
        if (image is not null)
        {
            ImageByPath.Add(file, image);
            Images.Remove(image);
        }
        return image;
    }

    /// <summary>
    /// 载入图片
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage? LoadImage(
        Stream stream,
        IEnableLogger? logger = null,
        bool endInit = true
    )
    {
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));
        var image = new BitmapImage();
        image.BeginInit();
        try
        {
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
        }
        catch (Exception ex)
        {
            logger?.Log().Warn(ex);
            image.StreamSource?.Close();
            image.StreamSource = null;
            image.EndInit();
            image.Freeze();
            image = null;
        }
        finally
        {
            if (endInit)
            {
                image?.EndInit();
                image?.Freeze();
            }
        }
        if (image is not null)
        {
            Images.Remove(image);
        }
        return image;
    }

    /// <summary>
    /// 载入图片至内存流
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage? LoadImageToMemory(
        string file,
        IEnableLogger? logger = null,
        bool endInit = true
    )
    {
        if (File.Exists(file) is false)
            return null;
        if (ImageByPath.TryGetValue(file, out var oldImage))
            return oldImage;
        var image = new BitmapImage();
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
        catch (Exception ex)
        {
            logger?.Log().Warn(ex);
            image.StreamSource?.Close();
            image.StreamSource = null;
            image.EndInit();
            image.Freeze();
            image = null;
        }
        finally
        {
            if (endInit)
            {
                image?.EndInit();
                image?.Freeze();
            }
        }
        if (image is not null)
        {
            ImageByPath.Add(file, image);
            Images.Add(image);
        }
        return image;
    }
}
