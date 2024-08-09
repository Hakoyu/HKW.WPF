using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// 标记扩展
/// </summary>
/// <typeparam name="T">值类型</typeparam>
/// <inheritdoc/>
/// <param name="value">值</param>
public abstract class MarkupExtension<T>(T value) : MarkupExtension
{
    /// <summary>
    /// 值
    /// </summary>
    [ConstructorArgument("value")]
    public T Value { get; set; } = value;

    /// <inheritdoc/>
    /// <param name="serviceProvider">服务提供者</param>
    /// <returns>值</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value!;
    }
}
