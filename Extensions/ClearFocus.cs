﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKW.WPF.Extensions;

public static partial class WPFExtensions
{
    /// <summary>
    /// 清除控件焦点
    /// </summary>
    /// <param name="element">控件</param>
    public static void ClearFocus(this UIElement element)
    {
        FocusManager.SetFocusedElement(FocusManager.GetFocusScope(element), null);
    }
}
