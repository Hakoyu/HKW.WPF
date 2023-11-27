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
            new FrameworkPropertyMetadata(null, SelectedItemsPropertyChangedCallback)
        );

    private static void SelectedItemsPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not ListBox element)
            return;
        InitializeSelectedItems(element);
        element.SelectionChanged += ListBox_SelectionChanged;

        static void InitializeSelectedItems(ListBox element)
        {
            if (GetSelectedItems(element) is not IList list)
                return;
            list.Clear();
            foreach (var item in element.SelectedItems)
                list.Add(item);
        }
        static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
    #endregion
}
