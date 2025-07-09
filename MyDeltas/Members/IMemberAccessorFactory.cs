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
    /// <typeparam name="TStructuralType"></typeparam>
    /// <returns></returns>
    IDictionary<string, IMemberAccessor<TStructuralType>> GetMembers<TStructuralType>();
    /// <summary>
    /// 设置成员访问器
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="members"></param>
    void SetMembers<TStructuralType>(IDictionary<string, IMemberAccessor<TStructuralType>> members);
}
