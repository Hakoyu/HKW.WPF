using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 是布尔转换器
/// </summary>
public class IsBoolConverter : NullToBoolConverter
{
    /// <summary>
    /// 目标值属性
    /// </summary>
    public static readonly DependencyProperty TargetValueProperty = DependencyProperty.Register(
        nameof(TargetValue),
        typeof(bool),
        typeof(IsBoolConverter),
        new PropertyMetadata(true)
    );

    /// <summary>
    /// 目标值
    /// </summary>
    [DefaultValue(false)]
    public bool TargetValue
    {
        get => (bool)GetValue(TargetValueProperty);
        set => SetValue(TargetValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    protected override object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (value?.Equals(TargetValue) is true)
            return true ^ IsInverted;
        return false ^ IsInverted;
    }
}
