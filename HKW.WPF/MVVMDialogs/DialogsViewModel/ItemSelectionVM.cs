using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using HKW.HKWReactiveUI;
using HKW.HKWUtils.Observable;

namespace HKW.WPF.MVVMDialogs;

/// <summary>
/// 可选中视图模型
/// </summary>
public partial class ItemSelectionVM : DialogWindowVM
{
    /// <inheritdoc/>
    /// <param name="collection">集合</param>
    /// <param name="seletedItem">选中项</param>
    public ItemSelectionVM(ICollection collection, object? seletedItem = default!)
    {
        Collection = collection;
        SelectedItem = seletedItem;
    }

    /// <inheritdoc/>
    /// <param name="collection">集合</param>
    /// <param name="seletedItems">选中项</param>
    public ItemSelectionVM(ICollection collection, IList seletedItems)
    {
        Collection = collection;
        SelectedItems = seletedItems;
    }

    ///// <summary>
    ///// 文本
    ///// </summary>
    //[ReactiveProperty]
    //public string? Text { get; set; } = null;

    /// <summary>
    /// 集合
    /// </summary>
    [ReactiveProperty]
    public ICollection Collection { get; set; }

    /// <summary>
    /// 选中项
    /// </summary>
    [ReactiveProperty]
    public object? SelectedItem { get; set; } = default;

    /// <summary>
    /// 选中项
    /// </summary>
    [ReactiveProperty]
    public IList? SelectedItems { get; set; } = null!;

    #region Command

    #endregion
}
