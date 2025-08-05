using System.Collections.Generic;

namespace MyDeltas;

/// <summary>
/// MyDelta 工厂
/// </summary>
/// <typeparam name="TInstance"></typeparam>
public interface IMyDeltaFactory<TInstance>
{
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="changed"></param>
    /// <returns>MyDelta 实例</returns>
    MyDelta<TInstance> Create(TInstance instance, IDictionary<string, object?> changed);
}
