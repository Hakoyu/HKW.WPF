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
    /// <param name="listBox"></param>
    /// <returns></returns>
    public static IList GetSelectedItems(ListBox listBox)
    {
        return (IList)listBox.GetValue(SelectedItemsProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用此方法</exception>
    public static void SetSelectedItems(ListBox listBox, IList value)
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
        if (obj is not ListBox listBox)
            return;
        InitializeSelectedItems(listBox);
        listBox.SelectionChanged += ListBox_SelectionChanged;

        static void InitializeSelectedItems(ListBox listBox)
        {
            if (GetSelectedItems(listBox) is not IList list)
                return;
            list.Clear();
            foreach (var item in listBox.SelectedItems)
                list.Add(item);
        }
        static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox listBox)
                return;
            if (GetSelectedItems(listBox) is not IList list)
                return;
            foreach (var item in e.RemovedItems)
                list.Remove(item);
            foreach (var item in e.AddedItems)
                list.Add(item);
        }
    }
    #endregion
}
