namespace HKW.WPF;

/// <summary>
/// 指定如何调整大小
/// </summary>
public enum ResizeMode
{
    /// <summary>
    /// 不调整大小
    /// </summary>
    NoResize = 0,

    /// <summary>
    /// 可最小化
    /// </summary>
    CanMinimize = 1,

    /// <summary>
    /// 可调整大小
    /// </summary>
    CanResize = 2,

    /// <summary>
    /// 可通过拖动边框调整大小
    /// </summary>
    CanResizeWithGrip = 3
}
