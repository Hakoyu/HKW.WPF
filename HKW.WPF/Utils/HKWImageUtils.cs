using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using HKW.HKWReactiveUI;
using HKW.HKWUtils.Collections;
using HKW.HKWUtils.Extensions;
using Splat;

namespace HKW.WPF;

/// <summary>
/// 图片工具
/// </summary>
public static class HKWImageUtils
{
    /// <summary>
    /// 所有外部图像 (Path, BitmapImage)
    /// </summary>
    public static BidirectionalDictionary<string, BitmapImage> ImageByPath { get; } = new([], []);

    /// <summary>
    /// 图像信息 (Path, BitmapImageInfo)
    /// </summary>
    public static Dictionary<string, BitmapImageInfo> InfoByPath { get; } = [];

    internal static void AddImage(string file, BitmapImage image)
    {
        ImageByPath.Add(file, image);
        InfoByPath.Add(file, new(file));
    }

    internal static bool TryGetInfo(
        string file,
        [MaybeNullWhen(false)] out BitmapImage image,
        [MaybeNullWhen(false)] out BitmapImageInfo info
    )
    {
        var result = ImageByPath.TryGetValue(file, out image);
        result = InfoByPath.TryGetValue(file, out info);
        return result;
    }

    internal static bool TryGetInfo(
        BitmapImage image,
        [MaybeNullWhen(false)] out BitmapImageInfo info
    )
    {
        info = null;
        var result = ImageByPath.TryGetValue(image, out var path);
        if (result is false)
            return false;
        result = InfoByPath.TryGetValue(path!, out info);
        return result;
    }

    /// <summary>
    /// 日志记录器
    /// </summary>
    public static IFullLogger? Logger { get; set; }

    /// <summary>
    /// 记录堆栈信息
    /// </summary>
    public static bool LogStackFrame { get; set; } = false;

    /// <summary>
    /// 载入图片
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage? LoadImage(string? file, bool endInit = true)
    {
        if (File.Exists(file) is false)
            return null;
        if (TryGetInfo(file, out var iimage, out var info))
        {
            info.ReferenceCount++;
            if (LogStackFrame)
            {
                Logger?.Debug(
                    "Image file {file}\nLoad again in\n{$StackFrame}\nReferenceCount : {ReferenceCount}",
                    file,
                    new StackFrame(1, true),
                    info.ReferenceCount
                );
            }
            else
            {
                Logger?.Debug(
                    "Image file {file}\nLoad again, ReferenceCount : {ReferenceCount}",
                    file,
                    info.ReferenceCount
                );
            }
            return iimage;
        }
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
            Logger?.Warn(ex);
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
            AddImage(file, image);
            if (LogStackFrame)
            {
                Logger?.Debug(
                    "Image file {file}\nFirst load in\n{$StackFrame}",
                    file,
                    new StackFrame(1, true)
                );
            }
            else
            {
                Logger?.Debug("Image file {file}\nFirst load", file);
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
    public static BitmapImage? LoadImage(Stream stream, bool endInit = true)
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
            Logger?.Warn(ex);
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
        return image;
    }

    /// <summary>
    /// 载入图片至内存流
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="endInit">结束初始化</param>
    /// <returns>图片</returns>
    public static BitmapImage? LoadImageToMemory(string file, bool endInit = true)
    {
        if (File.Exists(file) is false)
            return null;
        if (TryGetInfo(file, out var iimage, out var info))
        {
            info.ReferenceCount++;
            if (LogStackFrame)
            {
                Logger?.Debug(
                    "Image file {file}\nLoad again in\n{$StackFrame}\nReferenceCount : {ReferenceCount}",
                    file,
                    new StackFrame(1, true),
                    info.ReferenceCount
                );
            }
            else
            {
                Logger?.Debug(
                    "Image file {file}\nLoad again, ReferenceCount : {ReferenceCount}",
                    file,
                    info.ReferenceCount
                );
            }
            return iimage;
        }
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
            Logger?.Warn(ex);
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
            AddImage(file, image);
            if (LogStackFrame)
            {
                Logger?.Debug(
                    "Image file {file}\nFirst load in\n{$StackFrame}",
                    file,
                    new StackFrame(1, true)
                );
            }
            else
            {
                Logger?.Debug("Image file {file}\nFirst load", file);
            }
        }
        return image;
    }
}

/// <summary>
/// 图片信息
/// </summary>
/// <param name="path">图片路径</param>
[DebuggerDisplay("Count = {ReferenceCount}, Path = {Path}")]
public class BitmapImageInfo(string path) : IEquatable<BitmapImageInfo>
{
    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; } = path;

    /// <summary>
    /// 引用次数
    /// </summary>
    [ReactiveProperty]
    public int ReferenceCount { get; set; } = 1;

    #region IEquatable
    /// <inheritdoc/>
    public bool Equals(BitmapImageInfo? other)
    {
        return Path == other?.Path;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as BitmapImageInfo);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }
    #endregion
}
