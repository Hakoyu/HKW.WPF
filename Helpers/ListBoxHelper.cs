using System.Collections;

namespace HKW.WPF.Helpers;

/// <summary>
/// 列表框助手
/// </summary>
public static class ListBoxHelper
{
    #region SelectedItems
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static IList GetSelectedItems(ListBox element)
    {
        return (IList)element.GetValue(SelectedItemsProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用此方法</exception>
    public static void SetSelectedItems(ListBox element, IList value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWayToSource'."
        );
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(IList),
            typeof(ListBoxHelper),
            new FrameworkPropertyMetadata(default(IList), SelectedItemsPropertyChanged)
        );

    private static void SelectedItemsPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ListBox element)
            return;
        if (GetSelectedItems(element) is not IList list)
            return;
        list.Clear();
        foreach (var item in element.SelectedItems)
            list.Add(item);
        element.SelectionChanged += Element_SelectionChanged;
        element.Unloaded += Element_Unloaded;
        static void Element_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox element)
                return;
            if (GetSelectedItems(element) is not IList list)
                return;
            foreach (var item in e.RemovedItems)
                list.Remove(item);
            foreach (var item in e.AddedItems)
                list.Add(item);
        }
        static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not ListBox element)
                return;
            element.SelectionChanged -= Element_SelectionChanged;
            element.Unloaded -= Element_Unloaded;
        }
    }

    #endregion
}
