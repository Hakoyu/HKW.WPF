using HKW.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

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
            new FrameworkPropertyMetadata(default(string), CheckGroupPropertyChanged)
        );

    /// <summary>
    /// (TopParent ,(GroupName, CheckBoxGroup))
    /// </summary>
    private readonly static Dictionary<
        FrameworkElement,
        Dictionary<string, CheckBoxGroup>
    > _checkBoxGroups = new();

    private static void CheckGroupPropertyChanged(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not CheckBox checkBox)
            return;
        var groupName = GetCheckGroup(checkBox);
        var topParent = checkBox.FindTopParentOnVisualTree();
        // 在设计器中会无法获取顶级元素, 会提示错误, 忽略即可
        if (topParent is null)
            return;
        // 获取当前页面的分组
        if (_checkBoxGroups.ContainsKey(topParent) is false)
        {
            topParent.Loaded -= TopParent_Loaded;
            topParent.Unloaded -= TopParent_Unloaded;

            topParent.Loaded += TopParent_Loaded;
            topParent.Unloaded += TopParent_Unloaded;
        }
        checkBox.Loaded -= Element_Loaded;

        checkBox.Loaded += Element_Loaded;

        #region Element
        static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            var groups = _checkBoxGroups[topParent];
            if (groups.TryGetValue(groupName, out var group) is false)
                groups[groupName] = group = new();
            if (GetCheckGroupLeader(checkBox) == groupName)
            {
                if (group.Leader is not null)
                    throw new Exception("CheckBox group leader already exists");
                group.Leader = checkBox;
                group.LeaderLastChecked = checkBox.IsChecked;

                checkBox.Checked -= LeaderCheckBox_Checked;
                checkBox.Unchecked -= LeaderCheckBox_Unchecked;
                checkBox.Unloaded -= LeaderCheckBox_Unloaded;

                checkBox.Checked += LeaderCheckBox_Checked;
                checkBox.Unchecked += LeaderCheckBox_Unchecked;
                checkBox.Unloaded += LeaderCheckBox_Unloaded;
            }
            else
            {
                checkBox.Checked -= SubCheckBox_Checked;
                checkBox.Unchecked -= SubCheckBox_Unchecked;
                checkBox.Unloaded -= SubCheckBox_Unloaded;

                checkBox.Checked += SubCheckBox_Checked;
                checkBox.Unchecked += SubCheckBox_Unchecked;
                checkBox.Unloaded += SubCheckBox_Unloaded;

                group.SubCheckBoxes.Add(checkBox);
            }
        }
        #endregion

        #region TopParent
        static void TopParent_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            _checkBoxGroups[element] = new();
        }

        static void TopParent_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;
            _checkBoxGroups.Remove(element);
        }
        #endregion

        #region Leader
        static void LeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            var group = _checkBoxGroups[topParent][groupName];
            if (group.Changing)
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
                checkBox.IsChecked = null;
                GetAnyCanCheckFalseOnLeaderCheckedCommand(checkBox)?.Execute(groupName);
            }
            group.Changing = false;
        }

        static void LeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            var group = _checkBoxGroups[topParent][groupName];
            if (group.Changing)
                return;
            group.Changing = true;
            foreach (var item in group.SubCheckBoxes)
                item.IsChecked = false;
            group.Changing = false;
        }

        static void LeaderCheckBox_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            checkBox.Checked -= LeaderCheckBox_Checked;
            checkBox.Unchecked -= LeaderCheckBox_Unchecked;
            checkBox.Unloaded -= LeaderCheckBox_Unloaded;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            if (_checkBoxGroups.TryGetValue(topParent, out var groups) is false)
                return;
            var group = groups[groupName];
            if (group.Changing)
                return;
            foreach (var item in group.SubCheckBoxes)
            {
                item.Checked -= SubCheckBox_Checked;
                item.Unchecked -= SubCheckBox_Unchecked;
                item.Unloaded -= SubCheckBox_Unloaded;
            }
            groups.Remove(groupName);
        }
        #endregion

        #region Sub
        static void SubCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            var group = _checkBoxGroups[topParent][groupName];
            if (group.Changing)
                return;
            group.Changing = true;
            checkBox.IsChecked = GetCanCheck(checkBox);
            if (group.Leader is not null)
                group.Leader.IsChecked = group.SubIsAllChecked();
            group.Changing = false;
        }

        static void SubCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            var group = _checkBoxGroups[topParent][groupName];
            if (group.Changing)
                return;
            group.Changing = true;
            if (group.Leader is not null)
                group.Leader.IsChecked = group.SubIsAllChecked();
            group.Changing = false;
        }

        static void SubCheckBox_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            checkBox.Checked -= SubCheckBox_Checked;
            checkBox.Unchecked -= SubCheckBox_Unchecked;
            checkBox.Unloaded -= SubCheckBox_Unloaded;
            var groupName = GetCheckGroup(checkBox);
            var topParent = checkBox.FindTopParentOnVisualTree();
            if (_checkBoxGroups.TryGetValue(topParent, out var groups) is false)
                return;
            var group = groups[groupName];
            group.SubCheckBoxes.Remove(checkBox);
        }
        #endregion
    }

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
    public HashSet<CheckBox> SubCheckBoxes { get; set; } = new();

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
