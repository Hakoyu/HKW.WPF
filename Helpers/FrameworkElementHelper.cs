using HKW.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace HKW.WPF.Helpers;

public static class FrameworkElementHelper
{
    #region Uniform Width Group
    /// <summary>
    ///
    /// </summary>
    /// <param name="fe"></param>
    /// <returns></returns>
    public static string GetUniformWidthGroup(FrameworkElement fe)
    {
        return (string)fe.GetValue(UniformWidthGroupProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用次方法</exception>
    public static void SetUniformWidthGroup(FrameworkElement fe, string value)
    {
        fe.SetValue(UniformWidthGroupProperty, value);
    }

    public static readonly DependencyProperty UniformWidthGroupProperty =
        DependencyProperty.RegisterAttached(
            "UniformWidthGroup",
            typeof(string),
            typeof(FrameworkElementHelper),
            new FrameworkPropertyMetadata(null, UniformWidthGroupPropertyChangedCallback)
        );

    /// <summary>
    /// (Class.Name,(Class.HashCode, (GroupName, (Element.Name, Width))))
    /// </summary>
    private readonly static Dictionary<
        FrameworkElement,
        Dictionary<string, List<FrameworkElement>>
    > _domains = new();

    private static void UniformWidthGroupPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not FrameworkElement fe)
            return;
        if (string.IsNullOrEmpty(fe.Name))
            throw new ArgumentException("Must set x:Name");
        var feParent = GetDomainFE(fe);
        if (feParent == null)
            return;
        var currentDomain = _domains[feParent];
        var groupName = GetUniformWidthGroup(fe);
        currentDomain.TryAdd(groupName, new());
        var group = currentDomain[groupName];
        group.Add(fe);
        fe.SizeChanged += Fe_SizeChanged;
        fe.Unloaded += Fe_Unloaded;
        feParent.Unloaded += FeParent_Unloaded;
        ;
        return;
    }

    private static void FeParent_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        _domains.Remove(fe);
        //var currentDomain = _domains[GetDomainFE(fe)];
        //var groupName = GetUniformWidthGroup(fe);
        //currentDomain[groupName].Remove(fe);
    }

    private static void Fe_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        var currentDomain = _domains[GetDomainFE(fe)];
        var groupName = GetUniformWidthGroup(fe);
        currentDomain[groupName].Remove(fe);
    }

    private static void Fe_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;
        var currentDomain = _domains[GetDomainFE(fe)];
        var groupName = GetUniformWidthGroup(fe);
        var maxWidthFE = currentDomain[groupName].MaxBy(i => i.ActualWidth);
        if (maxWidthFE is null)
            return;
        if (maxWidthFE.ActualWidth == fe.ActualWidth)
            maxWidthFE = fe;
        foreach (var item in currentDomain[groupName])
            if (item != maxWidthFE)
                item.MinWidth = maxWidthFE.ActualWidth;
    }

    private static FrameworkElement GetDomainFE(FrameworkElement fe)
    {
        FrameworkElement feParent;
        if (fe.FindParent<Window>() is Window window)
        {
            feParent = window;
            _domains.TryAdd(window, new());
        }
        else if (fe.FindParent<Page>() is Page page)
        {
            feParent = page;
            _domains.TryAdd(page, new());
        }
        else
        {
            return null!;
        }
        return feParent;
    }
    #endregion
}

public class FrameworkElementWidthComparer : IComparer<FrameworkElement>
{
    public int Compare(FrameworkElement x, FrameworkElement y)
    {
        return x.ActualWidth.CompareTo(y.ActualWidth);
    }
}
