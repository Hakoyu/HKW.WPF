using System.Collections;
using System.Windows.Data;
using System.Windows.Input;

namespace HKW.WPF.Helpers;

/// <summary>
/// 输入框助手
/// </summary>
public static class TextBoxHelper
{
    #region UpdateBindingTextOnKeyDown
    /// <summary>
    ///
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string GetUpdateBindingTextOnKeyDown(TextBox element)
    {
        return (string)element.GetValue(UpdateBindingTextOnKeyDownProperty);
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception">禁止使用此方法</exception>
    public static void SetUpdateBindingTextOnKeyDown(TextBox element, string value)
    {
        throw new Exception(
            "This property is read-only. To bind to it you must use 'Mode=OneWayToSource'."
        );
    }

    /// <summary>
    /// 在按下按键时更新 <see cref="TextBox.Text"/> 的绑定数据
    /// <para>需要在<code><![CDATA[
    /// Text="{Binding Text, UpdateSourceTrigger=LostFocus}"
    /// ]]></code>
    /// 时使用 (<see cref="TextBox.Text"/>绑定属性默认 <see cref="Binding.UpdateSourceTrigger"/> 为 <see cref="UpdateSourceTrigger.LostFocus"/> )
    /// </para>
    /// </summary>
    public static readonly DependencyProperty UpdateBindingTextOnKeyDownProperty =
        DependencyProperty.RegisterAttached(
            "UpdateBindingTextOnKeyDown",
            typeof(string),
            typeof(TextBoxHelper),
            new FrameworkPropertyMetadata(null, UpdateBindingTextOnKeyDownPropertyChangedCallback)
        );

    private static void UpdateBindingTextOnKeyDownPropertyChangedCallback(
        DependencyObject obj,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (obj is not TextBox element)
            return;
        var keyName = GetUpdateBindingTextOnKeyDown(element);
        if (string.IsNullOrWhiteSpace(keyName))
            return;
        if (Enum.TryParse<Key>(keyName, false, out _) is false)
            throw new Exception($"Unknown key name {keyName}");
        element.KeyDown -= Control_KeyDown;
        element.KeyDown += Control_KeyDown;
    }

    private static void Control_KeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not TextBox element)
            return;
        var keyName = GetUpdateBindingTextOnKeyDown(element);
        var key = Enum.Parse<Key>(keyName);
        if (e.Key == key)
        {
            element.Focus();
            // 清除控件焦点
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(element), null);
            // 清除键盘焦点
            Keyboard.ClearFocus();
        }
    }
    #endregion
}
