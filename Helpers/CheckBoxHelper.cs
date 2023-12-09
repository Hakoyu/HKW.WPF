using HKW.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKW.WPF.Helpers;

/// <summary>
///
/// </summary>
public static class CheckBoxHelper
{
    #region CheckGroup
    #region AnyCanCheckFalseOnLeaderCheckedCommand
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static ICommand GetAnyCanCheckFalseOnLeaderCheckedCommand(CheckBox element)
    {
        return (ICommand)element.GetValue(AnyCanCheckFalseOnLeaderCheckedCommandProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value">命令</param>
    public static void SetAnyCanCheckFalseOnLeaderCheckedCommand(CheckBox element, ICommand value)
    {
        element.SetValue(AnyCanCheckFalseOnLeaderCheckedCommandProperty, value);
    }

    /// <summary>
    /// 当组长设置全组为 <see langword="true"/> 时失败触发的命令
    /// <para>
    /// 失败的原因是因为 CanCheck 有 <see langword="false"/>
    /// </para>
    /// <para>
    /// 仅在组长上设置才会触发
    /// </para>
    /// <para>
    /// 配合 CheckGroup 使用
    /// </para>
    /// </summary>
    public static readonly DependencyProperty AnyCanCheckFalseOnLeaderCheckedCommandProperty =
        DependencyProperty.RegisterAttached(
            "AnyCanCheckFalseOnLeaderCheckedCommand",
            typeof(ICommand),
            typeof(CheckBoxHelper),
            new FrameworkPropertyMetadata(default(ICommand))
        );
    #endregion

    #region CheckGroupLeader
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string GetCheckGroupLeader(CheckBox element)
    {
        return (string)element.GetValue(CheckGroupLeaderProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value">组名</param>
    public static void SetCheckGroupLeader(CheckBox element, string value)
    {
        element.SetValue(CheckGroupLeaderProperty, value);
    }

    /// <summary>
    /// 设为组长
    /// <para>
    /// 配合 CheckGroup 使用
    /// </para>
    /// </summary>
    public static readonly DependencyProperty CheckGroupLeaderProperty =
        DependencyProperty.RegisterAttached(
            "CheckGroupLeader",
            typeof(string),
            typeof(CheckBoxHelper),
            new FrameworkPropertyMetadata(default(string))
        );
    #endregion

    #region CanCheck
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool GetCanCheck(CheckBox element)
    {
        return (bool)element.GetValue(CanCheckProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value">是否能选中</param>
    public static void SetCanCheck(CheckBox element, bool value)
    {
        element.SetValue(CanCheckProperty, value);
    }

    /// <summary>
    /// 是否能被选中
    /// <para>
    /// 启用后触发的 Checked 事件将会被忽视
    /// </para>
    /// <para>
    /// 配合 CheckGroup 使用
    /// </para>
    /// </summary>
    public static readonly DependencyProperty CanCheckProperty =
        DependencyProperty.RegisterAttached(
            "CanCheck",
            typeof(bool),
            typeof(CheckBoxHelper),
            new FrameworkPropertyMetadata(true)
        );
    #endregion

    #region CheckGroupName
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string GetCheckGroup(CheckBox element)
    {
        return (string)element.GetValue(CheckGroupProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value">组名</param>
    public static void SetCheckGroup(CheckBox element, string value)
    {
        element.SetValue(CheckGroupProperty, value);
    }

    /// <summary>
    /// 设置复选框的分组
    /// <para>
    /// 其中 CheckGroupLeader 为组名的复选框会被设为主复选框, 无法同时设置两个主复选框
    /// </para>
    /// <para>
    /// 当子复选框改变时, 会改变主复选框, 反之亦然
    /// </para>
    /// </summary>
    public static readonly DependencyProperty CheckGroupProperty =
        DependencyProperty.RegisterAttached(
            "CheckGroup",
            typeof(string),
            typeof(CheckBoxHelper),
            new FrameworkPropertyMetadata(default(string), CheckGroupPropertyChangedCallback)
        );

    /// <summary>
    /// (TopParent ,(GroupName, CheckBoxGroup))
    /// </summary>
    private readonly static Dictionary<
        FrameworkElement,
        Dictionary<string, CheckBoxGroup>
    > _checkBoxGroups = new();

    private static void CheckGroupPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        // 获取当前页面的分组
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
        {
            _checkBoxGroups[topParent] = allGroup = new();
            topParent.Loaded += CheckGroup_TopParent_Loaded;
            topParent.Unloaded += CheckGroup_TopParent_Unloaded;
        }
        if (allGroup.TryGetValue(groupName, out var group) is false)
            allGroup[groupName] = group = new();
        group.SubCheckBoxes.Add(element);
    }

    #region TopParent
    private static void CheckGroup_TopParent_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;
        if (_checkBoxGroups.TryGetValue(element, out var groups) is false)
            return;
        foreach (var groupInfo in groups)
        {
            var group = groupInfo.Value;
            foreach (var item in group.SubCheckBoxes)
            {
                if (GetCheckGroupLeader(item) == groupInfo.Key)
                {
                    if (group.Leader is not null)
                        throw new Exception("CheckBox group leader already exists");
                    group.Leader = item;
                    group.LeaderLastChecked = item.IsChecked;
                    item.Checked += CheckGroup_Leader_Checked;
                    item.Unchecked += CheckGroup_Leader_Unchecked;
                    item.Unloaded += CheckGroup_Leader_Unloaded;
                }
                else
                {
                    item.Checked += CheckGroup_Sub_Checked;
                    item.Unchecked += CheckGroup_Sub_Unchecked;
                    item.Unloaded += CheckGroup_Sub_Unloaded;
                }
            }
            if (group.Leader is null)
                throw new Exception("CheckBox groupInfo leader not exists");
            group.SubCheckBoxes.Remove(group.Leader);
        }
        element.Loaded -= CheckGroup_TopParent_Loaded;
    }

    private static void CheckGroup_TopParent_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;
        _checkBoxGroups.Remove(element);
        element.Unloaded -= CheckGroup_TopParent_Unloaded;
    }
    #endregion

    #region Leader

    private static void CheckGroup_Leader_Unchecked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
            return;
        if (allGroup.TryGetValue(groupName, out var group) is false || group.Changing)
            return;
        group.Changing = true;
        foreach (var item in group.SubCheckBoxes)
            item.IsChecked = false;
        group.Changing = false;
    }

    private static void CheckGroup_Leader_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
            return;
        if (allGroup.TryGetValue(groupName, out var group) is false || group.Changing)
            return;
        group.Changing = true;

        var count = 0;
        foreach (var item in group.SubCheckBoxes)
        {
            if (GetCanCheck(item))
            {
                item.IsChecked = true;
                count++;
            }
        }
        if (count != group.SubCheckBoxes.Count)
        {
            element.IsChecked = null;
            GetAnyCanCheckFalseOnLeaderCheckedCommand(element)?.Execute(groupName);
        }
        group.Changing = false;
    }

    private static void CheckGroup_Leader_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
            return;
        if (allGroup.TryGetValue(groupName, out var group) is false)
            return;
        element.Checked += CheckGroup_Leader_Checked;
        element.Unchecked += CheckGroup_Leader_Unchecked;
        element.Unloaded -= CheckGroup_Leader_Unloaded;
        foreach (var item in group.SubCheckBoxes)
        {
            item.Checked -= CheckGroup_Sub_Checked;
            item.Unchecked -= CheckGroup_Sub_Unchecked;
            item.Unloaded -= CheckGroup_Sub_Unloaded;
        }
        allGroup.Remove(groupName);
    }
    #endregion

    #region Sub
    private static void CheckGroup_Sub_Unchecked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
            return;
        if (allGroup.TryGetValue(groupName, out var group) is false || group.Changing)
            return;
        group.Changing = true;
        group.Leader.IsChecked = group.SubIsAllChecked();
        group.Changing = false;
    }

    private static void CheckGroup_Sub_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
            return;
        if (allGroup.TryGetValue(groupName, out var group) is false || group.Changing)
            return;
        group.Changing = true;
        element.IsChecked = GetCanCheck(element);
        group.Leader.IsChecked = group.SubIsAllChecked();
        group.Changing = false;
    }

    private static void CheckGroup_Sub_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox element)
            return;
        var groupName = GetCheckGroup(element);
        var topParent = element.GetTopParent();
        if (_checkBoxGroups.TryGetValue(topParent, out var allGroup) is false)
            return;
        if (allGroup.TryGetValue(groupName, out var group) is false)
            return;
        group.SubCheckBoxes.Remove(element);
        element.Unloaded -= CheckGroup_Sub_Unloaded;
    }
    #endregion
    #endregion
    #endregion
}

/// <summary>
/// 复选框分组
/// </summary>
public class CheckBoxGroup
{
    /// <summary>
    /// 改变中
    /// </summary>
    public bool Changing { get; set; } = false;

    /// <summary>
    ///
    /// </summary>
    public bool? LeaderLastChecked { get; set; } = false;

    /// <summary>
    /// 主复选框
    /// </summary>
    public CheckBox Leader { get; set; } = null!;

    /// <summary>
    /// 复选框
    /// </summary>
    public List<CheckBox> SubCheckBoxes { get; set; } = new();

    /// <summary>
    /// 子复选框全部被选中
    /// </summary>
    /// <returns>全部选中为 <see langword="true"/> 全部未选中为 <see langword="false"/> 选中部分为 <see langword="null"/></returns>
    public bool? SubIsAllChecked()
    {
        var count = 0;
        foreach (var item in SubCheckBoxes)
            if (item.IsChecked is true)
                count++;
        if (count == 0)
            return false;
        else if (count == SubCheckBoxes.Count)
            return true;
        else
            return null;
    }
}
