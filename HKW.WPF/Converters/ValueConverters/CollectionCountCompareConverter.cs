using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <summary>
/// 集合为空转换器
/// </summary>
public class CollectionCountCompareConverter : InvertibleValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<int> DefeatCountProperty =
        CommonDependencyProperty.Register<CollectionCountCompareConverter, int>(
            nameof(DefeatCount),
            0
        );

    /// <summary>
    /// 集合参数数量
    /// </summary>
    public int DefeatCount
    {
        get => GetValue(DefeatCountProperty);
        set => SetValue(DefeatCountProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<ComparisonOperatorType> ComparisonTypeProperty =
        CommonDependencyProperty.Register<CollectionCountCompareConverter, ComparisonOperatorType>(
            nameof(ComparisonType),
            ComparisonOperatorType.Equality
        );

    /// <summary>
    /// 比较类型
    /// </summary>
    public ComparisonOperatorType ComparisonType
    {
        get => GetValue(ComparisonTypeProperty);
        set => SetValue(ComparisonTypeProperty, value);
    }

    /// <inheritdoc/>
    public CollectionCountCompareConverter()
    {
        CommonValueConverter = new CommonValueConverters.CollectionCountCompareConverter()
        {
            GetDefeatCount = () => DefeatCount,
            GetComparisonType = () => ComparisonType,
        };
    }
}
