using System.Collections.Generic;

namespace MyDeltas;

/// <summary>
/// MyDelta 工厂
/// </summary>
public interface IMyDeltaFactory
{
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="instance"></param>
    /// <param name="changed"></param>
    /// <returns>MyDelta 实例</returns>
    MyDelta<TStructuralType> Create<TStructuralType>(TStructuralType instance, IDictionary<string, object?> changed);
}
