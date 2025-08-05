using System.Collections.Generic;

namespace MyDeltas.Members;

/// <summary>
/// 成员访问器工厂
/// </summary>
public interface IMemberAccessorFactory
{
    /// <summary>
    /// 创建成员访问器
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <returns></returns>
    IDictionary<string, IMemberAccessor<TInstance>> GetMembers<TInstance>();
    /// <summary>
    /// 设置成员访问器
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="members"></param>
    void SetMembers<TInstance>(IDictionary<string, IMemberAccessor<TInstance>> members);
}
