using System;
using System.Collections.Generic;

namespace MyDeltas.Members;

/// <summary>
/// 委托成员构造器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="members"></param>
public class DelegateBuilder<TStructuralType>(IDictionary<string, IMemberAccessor<TStructuralType>> members)
    : IMyDeltaFactory<TStructuralType>
{
    /// <summary>
    /// 委托成员构造器
    /// </summary>
    public DelegateBuilder()
        : this(new Dictionary<string, IMemberAccessor<TStructuralType>>())
    {
    }
    #region 配置
    private readonly IDictionary<string, IMemberAccessor<TStructuralType>> _members = members;
    /// <summary>
    /// 成员
    /// </summary>
    public IDictionary<string, IMemberAccessor<TStructuralType>> Members 
        => _members;
    #endregion
    #region 方法
    /// <summary>
    /// 添加成员访问器
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="getter"></param>
    /// <param name="setter"></param>
    public DelegateBuilder<TStructuralType> Add<TMember>(string memberName, Func<TStructuralType, TMember> getter, Action<TStructuralType, TMember> setter)
    {
        _members[memberName] = new DelegateAccessor<TStructuralType, TMember>(getter, setter);
        return this;
    }
    /// <summary>
    /// 构造MyDelta
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="changed"></param>
    /// <returns></returns>
    public MyDelta<TStructuralType> Create(TStructuralType instance, IDictionary<string, object?> changed)
        => new(instance, _members, changed);
    #endregion
}
