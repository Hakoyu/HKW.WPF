using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.CollectionCountCompareConverter"/>
public class CollectionCountCompareConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public CollectionCountCompareConverter()
    {
        CommonValueConverter = new CommonValueConverters.CollectionCountCompareConverter();
    }
}

/// <inheritdoc cref="CommonValueConverters.CollectionCountCompareByConverter"/>
public class CollectionCountCompareByConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public CollectionCountCompareByConverter()
    {
        CommonValueConverter = new CommonValueConverters.CollectionCountCompareByConverter();
    }
}
