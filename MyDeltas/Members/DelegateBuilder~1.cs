using System;
using System.Collections.Generic;

namespace MyDeltas.Members;

/// <summary>
/// 委托成员构造器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <param name="members"></param>
public class DelegateBuilder<TInstance>(IDictionary<string, IMemberAccessor<TInstance>> members)
    : IMyDeltaFactory<TInstance>
{
    /// <summary>
    /// 委托成员构造器
    /// </summary>
    public DelegateBuilder()
        : this(new Dictionary<string, IMemberAccessor<TInstance>>())
    {
    }
    #region 配置
    private readonly IDictionary<string, IMemberAccessor<TInstance>> _members = members;
    /// <summary>
    /// 成员
    /// </summary>
    public IDictionary<string, IMemberAccessor<TInstance>> Members 
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
    public DelegateBuilder<TInstance> Add<TMember>(string memberName, Func<TInstance, TMember> getter, Action<TInstance, TMember> setter)
    {
        _members[memberName] = new DelegateAccessor<TInstance, TMember>(getter, setter);
        return this;
    }
    /// <summary>
    /// 构造MyDelta
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="changed"></param>
    /// <returns></returns>
    public MyDelta<TInstance> Create(TInstance instance, IDictionary<string, object?> changed)
        => new(instance, _members, changed);
    #endregion
}
