using System;
using System.Collections;
using System.Collections.Generic;
#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace MyDeltas.Members;

/// <summary>
/// 成员访问器工厂基类(缓存)
/// </summary>
public abstract class MemberAccessorFactoryBase(IEqualityComparer<string> memberComparer)
    : IMemberAccessorFactory, IMyDeltaFactory
{
    /// <summary>
    /// 默认成员访问器工厂
    /// </summary>
    public MemberAccessorFactoryBase()
        : this(StringComparer.Ordinal)
    {
    }
    #region 配置
    private readonly IEqualityComparer<string> _memberComparer = memberComparer;
    /// <summary>
    /// 成员匹配器
    /// </summary>
    public IEqualityComparer<string> MemberComparer
        => _memberComparer;
#if NET9_0_OR_GREATER
    private readonly Lock _cacherLock = new();
#endif
    private readonly Dictionary<Type, IDictionary> _cacher = [];
    #endregion
    /// <inheritdoc />
    public MyDelta<TInstance> Create<TInstance>(TInstance instance, IDictionary<string, object?> changed)
        => new(instance, GetMembers<TInstance>(), changed);
    /// <inheritdoc />
    public IDictionary<string, IMemberAccessor<TInstance>> GetMembers<TInstance>()
    {
        if (_cacher.TryGetValue(typeof(TInstance), out var cached) && cached is IDictionary<string, IMemberAccessor<TInstance>> members0)
            return members0;
#if NET9_0_OR_GREATER
        lock (_cacherLock)
#else
        lock (_cacher)
#endif
        {
            if (_cacher.TryGetValue(typeof(TInstance), out cached) && cached is IDictionary<string, IMemberAccessor<TInstance>> members2)
                return members2;
            Dictionary<string, IMemberAccessor<TInstance>> members = new(_memberComparer);
            CheckMembers(members);
            _cacher[typeof(TInstance)] = members;
            return members;
        }
    }
    /// <summary>
    /// 初始化成员访问器
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="members"></param>
    protected abstract void CheckMembers<TInstance>(IDictionary<string, IMemberAccessor<TInstance>> members);
    /// <summary>
    /// 缓存成员访问器
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="members"></param>
    private void SetCache<TInstance>(IDictionary<string, IMemberAccessor<TInstance>> members)
    {
        if (members is IDictionary dic)
        {
            CheckMembers(members);
            _cacher[typeof(TInstance)] = dic;
        }
    }
    void IMemberAccessorFactory.SetMembers<TInstance>(IDictionary<string, IMemberAccessor<TInstance>> members)
        => SetCache(members);
}
