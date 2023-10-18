using HKW.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class FrameworkElementHelper
{
    #region UniformMinWidthGroup
    /// <summary>
    ///
    /// </summary>
    /// <param name="fe"></param>
    /// <returns></returns>
    public static string GetUniformMinWidthGroup(FrameworkElement fe)
    {
        return (string)fe.GetValue(UniformWidthGroupProperty);
    }

    /// <summary>
    ///
    /// </summary>
    public static void SetUniformMinWidthGroup(FrameworkElement fe, string value)
    {
        fe.SetValue(UniformWidthGroupProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty UniformWidthGroupProperty =
        DependencyProperty.RegisterAttached(
            "UniformMinWidthGroup",
            typeof(string),
            typeof(FrameworkElementHelper),
            new FrameworkPropertyMetadata(null, UniformMinWidthGroupPropertyChanged)
        );

    /// <summary>
    /// (Domain ,(GroupName, FEInfos))
    /// </summary>
    private readonly static Dictionary<
        FrameworkElement,
        Dictionary<string, FEInfos>
    > rs_uniformMinWidthGroupDomains = new();

    private static void UniformMinWidthGroupPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not FrameworkElement fe)
            return;
        var feDomain = GetDomain(fe);
        if (feDomain == null)
            return;
        var currentDomain = rs_uniformMinWidthGroupDomains[feDomain];
        var groupName = GetUniformMinWidthGroup(fe);
        currentDomain.TryAdd(groupName, new());
        var currentGroup = currentDomain[groupName];
        fe.Tag = false;
        currentGroup.FEs.Add(fe);
        fe.SizeChanged += UniformMinWidthGroup_FE_SizeChanged;
        fe.Unloaded += UniformMinWidthGroup_FE_Unloaded;
        if (feDomain is Window window)
            window.Closed += UniformMinWidthGroup_Window_Closed;
        else
            feDomain.Unloaded += UniformMinWidthGroup_Domain_Unloaded;
    }

    private static void UniformMinWidthGroup_Window_Closed(object? sender, EventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        rs_uniformMinWidthGroupDomains.Remove(fe);
    }

    private static void UniformMinWidthGroup_Domain_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        rs_uniformMinWidthGroupDomains.Remove(fe);
    }

    private static void UniformMinWidthGroup_FE_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        var currentDomain = rs_uniformMinWidthGroupDomains[GetDomain(fe)];
        var groupName = GetUniformMinWidthGroup(fe);
        currentDomain[groupName].FEs.Remove(fe);
    }

    private static void UniformMinWidthGroup_FE_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        var domain = GetDomain(fe);
        var currentDomain = rs_uniformMinWidthGroupDomains[domain];
        var groupName = GetUniformMinWidthGroup(fe);
        var currentGroup = currentDomain[groupName];
        var maxWidthFE = currentDomain[groupName].FEs.MaxBy(i => i.ActualWidth);
        if (maxWidthFE is null)
            return;
        if (maxWidthFE.ActualWidth == fe.ActualWidth)
            maxWidthFE = fe;
        if (maxWidthFE.ActualWidth > currentGroup.LastMax)
        {
            // 如果当前控件最大宽度的超过历史最大宽度, 表面非最大宽度列表中的控件超过了历史最大宽度
            foreach (var item in currentGroup.FEs)
                item.MinWidth = maxWidthFE.ActualWidth;
            // 将当前控件最小宽度设为0
            maxWidthFE.MinWidth = 0;
            currentGroup.MaxFEs.Clear();
            // 设为最大宽度的唯一控件
            currentGroup.MaxFEs.Add(maxWidthFE);
            currentGroup.LastMax = maxWidthFE.ActualWidth;
        }
        else if (currentGroup.MaxFEs.Count == 1)
        {
            var current = currentGroup.MaxFEs.First();
            // 当最大宽度控件只有一个时, 并且当前控件宽度小于历史最大宽度时, 表明需要降低全体宽度
            if (currentGroup.LastMax > current.ActualWidth)
            {
                // 最小宽度设为0以自适应宽度
                foreach (var item in currentGroup.FEs)
                    item.MinWidth = 0;
                // 清空最大宽度列表, 让其刷新
                currentGroup.MaxFEs.Clear();
            }
        }
        else
        {
            // 将 FEInfos.LastMax 设置为 double.MaxValue 时, 可以让首次加载时进入此处
            foreach (var item in currentGroup.FEs)
            {
                // 当控件最小宽度为0(表示其为主导宽度的控件), 并且其宽度等于最大宽度, 加入最大宽度列表
                if (item.MinWidth == 0 && item.ActualWidth == maxWidthFE.ActualWidth)
                {
                    currentGroup.MaxFEs.Add(item);
                }
                else
                {
                    // 如果不是, 则从最大宽度列表删除, 并设置最小宽度为当前最大宽度
                    currentGroup.MaxFEs.Remove(item);
                    item.MinWidth = maxWidthFE.ActualWidth;
                }
            }
            currentGroup.LastMax = maxWidthFE.ActualWidth;
        }
    }

    private static FrameworkElement GetDomain(FrameworkElement fe)
    {
        FrameworkElement feParent;
        if (fe.FindParent<Window>() is Window window)
        {
            feParent = window;
            rs_uniformMinWidthGroupDomains.TryAdd(window, new());
        }
        else if (fe.FindParent<Page>() is Page page)
        {
            feParent = page;
            rs_uniformMinWidthGroupDomains.TryAdd(page, new());
        }
        else
        {
            return null!;
        }
        return feParent;
    }
    #endregion
}

/// <summary>
/// 控件信息
/// </summary>
public class FEInfos
{
    /// <summary>
    /// 所有控件
    /// </summary>
    public HashSet<FrameworkElement> FEs { get; } = new();

    /// <summary>
    /// 最后一个最大宽度
    /// </summary>
    public double LastMax { get; set; } = double.MaxValue;

    /// <summary>
    /// 最大宽度的控件
    /// </summary>
    public HashSet<FrameworkElement> MaxFEs { get; } = new();
}
