namespace MyDeltas.Members;

/// <summary>
/// 成员访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
public interface IMemberAccessor<TStructuralType>
{
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    object? GetValue(TStructuralType instance);
    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    void SetValue(TStructuralType instance, object? value);
    /// <summary>
    /// 复制值
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    void Copy(TStructuralType from, TStructuralType to);
}
