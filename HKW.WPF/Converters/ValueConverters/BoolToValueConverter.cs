using System.Globalization;
using System.Windows;
using System.Windows.Media;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// bool到可见度转换器
/// </summary>
public class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
{
    /// <inheritdoc/>
    public BoolToVisibilityConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
    }
}

/// <summary>
/// null到bool转换器
/// </summary>
public class NullToBoolConverter : BoolToValueConverter<bool>
{
    /// <inheritdoc/>
    public NullToBoolConverter()
    {
        TrueValue = false;
        FalseValue = false;
        NullValue = true;
        IsNullable = true;
    }
}

/// <summary>
/// bool反转转换器
/// </summary>
public class BoolInverteConverter : BoolToValueConverter<bool>
{
    /// <inheritdoc/>
    public BoolInverteConverter()
    {
        TrueValue = true;
        FalseValue = false;
        NullValue = false;
        IsInverted = true;
    }
}

/// <summary>
/// bool到字体粗细转换器
/// </summary>
public class BoolToFontWeightConverter : BoolToValueConverter<FontWeight>
{
    /// <inheritdoc/>
    public BoolToFontWeightConverter()
    {
        TrueValue = FontWeights.Bold;
        FalseValue = FontWeights.Normal;
    }
}

/// <summary>
/// bool到纯色笔刷转换器
/// </summary>
public class BoolToSolidColorBrushConverter : BoolToValueConverter<SolidColorBrush>
{
    /// <summary>
    ///
    /// </summary>
    public BoolToSolidColorBrushConverter()
    {
        TrueValue = Brushes.White;
        FalseValue = Brushes.Black;
    }
}

/// <summary>
/// bool到字符串转换器
/// </summary>
public class BoolToStringConverter : BoolToValueConverter<string>
{
    /// <inheritdoc/>
    public BoolToStringConverter()
    {
        DefaultResult = string.Empty;
    }
}

/// <summary>
/// bool到双精度浮点转换器
/// </summary>
public class BoolToDoubleConverter : BoolToValueConverter<double>
{
    /// <inheritdoc/>
    public BoolToDoubleConverter()
    {
        DefaultResult = 0.0;
    }
}

/// <summary>
/// bool到Int32转换器
/// </summary>
public class BoolToInt32Converter : BoolToValueConverter<int>
{
    /// <inheritdoc/>
    public BoolToInt32Converter()
    {
        DefaultResult = 0;
    }
}

/// <summary>
/// bool到厚度转换器
/// </summary>
public class BoolToThicknessConverter : BoolToValueConverter<Thickness>
{
    /// <inheritdoc/>
    public BoolToThicknessConverter()
    {
        DefaultResult = new Thickness(0);
    }
}

/// <summary>
/// bool到样式转换器
/// </summary>
public class BoolToStyleConverter : BoolToValueConverter<Style> { }

/// <summary>
/// bool到值转换器
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public class BoolToValueConverter<T> : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public BoolToValueConverter()
    {
        CommonValueConverter = new CommonValueConverters.BoolToValueConverter<T>()
        {
            GetTrueValue = () => TrueValue,
            GetFalseValue = () => FalseValue,
            GetNullValue = () => NullValue,
            GetIsNullable = () => IsNullable,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> TrueValueProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, T>(nameof(TrueValue));

    /// <summary>
    /// 为真时的值
    /// </summary>
    public T TrueValue
    {
        get => GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> FalseValueProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, T>(nameof(FalseValue));

    /// <summary>
    /// 为假时的值
    /// </summary>
    public T FalseValue
    {
        get => GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> IsNullableProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, bool>(nameof(IsNullable));

    /// <summary>
    /// 是可为空的
    /// </summary>
    public bool IsNullable
    {
        get => GetValue(IsNullableProperty);
        set => SetValue(IsNullableProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<T> NullValueProperty =
        CommonDependencyProperty.Register<BoolToValueConverter<T>, T>(nameof(NullValue));

    /// <summary>
    /// 为空时的bool值
    /// </summary>
    public T NullValue
    {
        get => GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }
}
