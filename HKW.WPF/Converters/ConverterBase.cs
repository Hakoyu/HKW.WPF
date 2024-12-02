using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
///  通用附加属性
/// </summary>
public class CommonDependencyProperty
{
    /// <summary>
    /// 注册附加属性
    /// </summary>
    /// <typeparam name="TOwner">父类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="propertyName">属性名</param>
    /// <returns>附加属性</returns>
    public static CommonDependencyProperty<TProperty> Register<TOwner, TProperty>(
        string propertyName
    )
    {
#if DEBUG
        var method = new StackTrace(1).GetFrame(0)?.GetMethod();
        if (method?.DeclaringType?.Name != typeof(TOwner).Name)
            throw new NotImplementedException();
#endif
        var dependencyProperty = DependencyProperty.Register(
            propertyName,
            typeof(TProperty),
            typeof(TOwner)
        );
        return new(dependencyProperty);
    }

    /// <summary>
    /// 注册附加属性
    /// </summary>
    /// <typeparam name="TOwner">父类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="propertyName">属性名称</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>附加属性</returns>
    public static CommonDependencyProperty<TProperty> Register<TOwner, TProperty>(
        string propertyName,
        TProperty defaultValue
    )
    {
#if DEBUG
        var method = new StackTrace(1).GetFrame(0)?.GetMethod();
        if (method?.DeclaringType?.Name != typeof(TOwner).Name)
            throw new NotImplementedException();
#endif
        var dependencyProperty = DependencyProperty.Register(
            propertyName,
            typeof(TProperty),
            typeof(TOwner),
            new PropertyMetadata(defaultValue)
        );
        return new(dependencyProperty);
    }
}

/// <summary>
/// 转换器基类
/// </summary>
public abstract class ConverterBase : DependencyObject, ICommonValueConverter
{
    /// <inheritdoc/>
    protected ConverterBase()
    {
        CommonConverterBase.UnsetValue = DependencyProperty.UnsetValue;
    }

    /// <summary>
    /// 未设置值
    /// </summary>
    public static readonly object UnsetValue = DependencyProperty.UnsetValue;

    /// <inheritdoc/>
    public T GetValue<T>(CommonDependencyProperty<T> commonDependencyProperty)
    {
        if (commonDependencyProperty.Value is not DependencyProperty dependency)
            throw new ArgumentNullException(nameof(commonDependencyProperty));
        return (T)GetValue(dependency);
    }

    /// <inheritdoc/>
    public void SetValue<T>(CommonDependencyProperty<T> commonDependencyProperty, T value)
    {
        if (commonDependencyProperty.Value is not DependencyProperty dependency)
            throw new ArgumentNullException(nameof(commonDependencyProperty));
        SetValue(dependency, value);
    }

    #region DefaultResult
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<object> DefaultResultProperty =
        CommonDependencyProperty.Register<ConverterBase, object>(nameof(DefaultResult));

    /// <summary>
    /// 默认结果
    /// </summary>
    public object DefaultResult
    {
        get => GetValue(DefaultResultProperty);
        set => SetValue(DefaultResultProperty, value);
    }
    #endregion

    #region PreferredCulture
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<PreferredCulture> PreferredCultureProperty =
        CommonDependencyProperty.Register<ConverterBase, PreferredCulture>(
            nameof(PreferredCulture)
        );

    /// <summary>
    /// 默认结果
    /// </summary>
    public PreferredCulture PreferredCulture
    {
        get => GetValue(PreferredCultureProperty);
        set => SetValue(PreferredCultureProperty, value);
    }
    #endregion
}
