namespace MyDeltas.Members;

/// <summary>
/// 成员访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
public interface IMemberAccessor<TInstance>
{
    /// <summary>
    /// 判断值是否变更
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool CheckChange(TInstance instance, object? value);
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    object? GetValue(TInstance instance);
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
    bool TrySetValue(TInstance instance, object? value);
    /// <summary>
    /// 修改值
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    void SetValue(TInstance instance, object? value);
    /// <summary>
    /// 复制值
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    void Copy(TInstance from, TInstance to);
}
