namespace MyDeltas.Members;

/// <summary>
/// 成员访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
public interface IMemberAccessor<TStructuralType>
{
    /// <summary>
    /// 判断值是否变更
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool CheckChange(TStructuralType instance, object? value);
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    object? GetValue(TStructuralType instance);
    /// <summary>
    /// 检查值类型
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    object? CheckValue(object? value);
    /// <summary>
    /// 尝试修改值
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TrySetValue(TStructuralType instance, object? value);
    /// <summary>
    /// 修改值
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
