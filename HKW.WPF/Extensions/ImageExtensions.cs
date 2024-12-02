using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HKW.HKWUtils.Extensions;
using HKW.WPF;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 添加引用计数
    /// </summary>
    /// <param name="image">图像</param>
    public static BitmapImage AddReferenceCount(this BitmapImage image)
    {
        if (HKWImageUtils.TryGetInfo(image, out var info) is false)
            throw new ArgumentException("Not have image", nameof(image));
        info.ReferenceCount++;
        if (HKWImageUtils.LogStackFrame)
        {
            HKWImageUtils.Logger?.Debug(
                "Image file {file}\nAdd reference count in\n{$StackFrame}\nReferenceCount: {ReferenceCount}",
                info.Path,
                new StackFrame(1, true),
                info.ReferenceCount
            );
        }
        else
        {
            HKWImageUtils.Logger?.Debug(
                "Image file {file}\nAdd ReferenceCount : {ReferenceCount}",
                info.Path,
                info.ReferenceCount
            );
        }
        return image;
    }

    /// <summary>
    /// 当没有引用时关闭流,否则仅减少引用次数
    /// </summary>
    /// <param name="image">图像资源</param>
    /// <param name="flush">清除缓存</param>
    public static void CloseStreamWhenNoReference(this BitmapImage image, bool flush = false)
    {
        if (image.StreamSource is null)
            return;
        if (HKWImageUtils.TryGetInfo(image, out var info) is false)
            throw new ArgumentException("Not have image", nameof(image));
        info.ReferenceCount--;
        if (HKWImageUtils.LogStackFrame)
        {
            HKWImageUtils.Logger?.Debug(
                "Image file {file}\nIs close when no reference in\n{$StackFrame}\nReferenceCount: {ReferenceCount}",
                info.Path,
                new StackFrame(1, true),
                info.ReferenceCount
            );
        }
        else
        {
            HKWImageUtils.Logger?.Debug(
                "Image file {file}\nIs close when no reference, ReferenceCount : {ReferenceCount}",
                info.Path,
                info.ReferenceCount
            );
        }

        if (info.ReferenceCount <= 0)
        {
            image.StreamSource.Close();
            if (flush)
                image.StreamSource.Flush();
            HKWImageUtils.ImageByPath.Remove(image);
            HKWImageUtils.InfoByPath.Remove(info.Path);
        }
    }

    /// <summary>
    /// 关闭流
    /// </summary>
    /// <param name="image">图像资源</param>
    /// <param name="flush">清除缓存</param>
    public static void CloseStream(this BitmapImage image, bool flush = false)
    {
        if (image.StreamSource is null)
            return;
        image.StreamSource.Close();
        if (flush)
            image.StreamSource.Flush();
        if (HKWImageUtils.TryGetInfo(image, out var info))
        {
            HKWImageUtils.ImageByPath.Remove(image);
            HKWImageUtils.InfoByPath.Remove(info.Path);
            if (HKWImageUtils.LogStackFrame)
            {
                HKWImageUtils.Logger?.Debug(
                    "Image file {file}\nIs close in\n{$StackFrame}",
                    info.Path,
                    info.ReferenceCount,
                    new StackFrame(1, true)
                );
            }
            else
            {
                HKWImageUtils.Logger?.Debug(
                    "Image file {file}\nIs close",
                    info.Path,
                    info.ReferenceCount
                );
            }
        }
    }

    /// <inheritdoc cref="Bitmap.SetResolution(float, float)"/>
    public static void SetResolution(this Bitmap bitmap, float dpi)
    {
        bitmap.SetResolution(dpi, dpi);
    }

    /// <summary>
    /// 使用流克隆一个新的图像
    /// </summary>
    /// <param name="image">图像</param>
    /// <param name="beginInitAction">初始化行动</param>
    /// <returns>克隆的图像</returns>
    public static BitmapImage? CloneFromStream(
        this BitmapImage? image,
        Action<BitmapImage>? beginInitAction = null
    )
    {
        if (image?.StreamSource is null)
            return null;
        var newImage = new BitmapImage();
        var p = image.StreamSource.Position;
        image.StreamSource.Position = 0;
        newImage.BeginInit();
        try
        {
            if (image.StreamSource is FileStream fileStream)
            {
                newImage.StreamSource = new FileStream(
                    fileStream.Name,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite
                );
            }
            else if (image.StreamSource is MemoryStream memoryStream)
            {
                var ms = new MemoryStream();
                memoryStream.CopyTo(ms);
                newImage.StreamSource = ms;
            }
            else
            {
                return null;
            }
            beginInitAction?.Invoke(image);
        }
        catch
        {
            newImage.StreamSource?.Close();
            newImage.StreamSource = null;
        }
        finally
        {
            newImage.EndInit();
            newImage.Freeze();
        }
        image.StreamSource.Position = p;
        return newImage;
    }

    /// <summary>
    /// 转换为位图
    /// </summary>
    /// <param name="image">图像</param>
    /// <returns>位图图像</returns>
    public static Bitmap? ToBitmap(this BitmapImage image)
    {
        if (image.StreamSource is null)
            return null;
        var p = image.StreamSource.Position;
        image.StreamSource.Position = 0;
        var bitmap = new Bitmap(image.StreamSource);
        image.StreamSource.Position = p;
        return bitmap;
    }

    /// <summary>
    /// 转化为位图图像
    /// </summary>
    /// <param name="bitmap">位图</param>
    /// <param name="imageFormat">图片格式 (为空则使用PNG)</param>
    /// <param name="disposeBitmap">处理位图</param>
    /// <returns>位图图像</returns>
    public static BitmapImage ToBitmapImage(
        this Bitmap bitmap,
        ImageFormat? imageFormat = null,
        bool disposeBitmap = true
    )
    {
        var ms = new MemoryStream();
        bitmap.Save(ms, imageFormat ?? ImageFormat.Png);
        if (disposeBitmap)
            bitmap.Dispose();
        return HKWImageUtils.LoadImage(ms)!;
    }

    /// <summary>
    /// 保存为
    /// </summary>
    /// <param name="image">图像</param>
    /// <param name="file">文件</param>
    /// <param name="extensionName">图片扩展名</param>
    public static void SaveTo(this BitmapImage image, string file, string extensionName)
    {
        if (image.StreamSource is null)
            return;
        file = Path.ChangeExtension(file, extensionName);
        var stream = image.StreamSource;
        // 保存位置
        var p = stream.Position;
        // 必须要重置位置, 否则EndInit将出错
        stream.Position = 0;
        using var fs = new FileStream(file, FileMode.Create);
        stream.CopyTo(fs);
        // 恢复位置
        stream.Position = p;
    }

    /// <summary>
    /// 保存为
    /// </summary>
    /// <param name="image">图像</param>
    /// <param name="file">文件</param>
    /// <param name="imageFormat">图片格式</param>
    public static bool SaveTo(this BitmapImage image, string file, ImageFormat imageFormat)
    {
        if (image.StreamSource is null)
            return false;
        file = Path.ChangeExtension(file, imageFormat.ToString().ToLower());
        using var bitmap = image.ToBitmap();
        if (bitmap is null)
            return false;
        bitmap.Save(file, imageFormat);
        return true;
    }
}
