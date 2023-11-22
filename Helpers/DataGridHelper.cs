using System.Collections;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class DataGridHelper
{
    #region SelectedItems
    /// <summary>
    ///
    /// </summary>
    /// <param name="dataGrid"></param>
    /// <returns></returns>
    public static IList GetSelectedItems(DataGrid dataGrid)
    {
        return (IList)dataGrid.GetValue(SelectedItemsProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用此方法</exception>
    public static void SetSelectedItems(DataGrid dataGrid, IList value)
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
            typeof(DataGridHelper),
            new FrameworkPropertyMetadata(null, SelectedItemsPropertyChangedCallback)
        );

    private static void SelectedItemsPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not DataGrid dataGrid)
            return;
        InitializeSelectedItems(dataGrid);
        dataGrid.SelectionChanged += DataGrid_SelectionChanged;

        static void InitializeSelectedItems(DataGrid dataGrid)
        {
            if (GetSelectedItems(dataGrid) is not IList list)
                return;
            list.Clear();
            foreach (var item in dataGrid.SelectedItems)
                list.Add(item);
        }
        static void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not DataGrid dataGrid)
                return;
            if (GetSelectedItems(dataGrid) is not IList list)
                return;
            foreach (var item in e.RemovedItems)
                list.Remove(item);
            foreach (var item in e.AddedItems)
                list.Add(item);
        }
    }
    #endregion
}
