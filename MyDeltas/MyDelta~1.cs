using MyDeltas.Members;
using System;
using System.Collections.Generic;

namespace MyDeltas;

/// <summary>
/// 数据变化
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="instance"></param>
/// <param name="members"></param>
/// <param name="changed"></param>
public class MyDelta<TStructuralType>(TStructuralType instance, IDictionary<string, IMemberAccessor<TStructuralType>> members,  IDictionary<string, object?> changed)
    : MyDelta(changed)
{
    /// <summary>
    /// 数据变化
    /// </summary>
    /// <param name="properties"></param>
    public MyDelta(IDictionary<string, IMemberAccessor<TStructuralType>> properties)
        : this(Activator.CreateInstance<TStructuralType>(), properties, new Dictionary<string, object?>())
    {
    }
    #region 配置
    private readonly IDictionary<string, IMemberAccessor<TStructuralType>> _members = members;
    private readonly TStructuralType _instance = instance;
    /// <summary>
    /// 实体
    /// </summary>
    public TStructuralType Instance
        => _instance;
    #endregion
    #region 方法
    /// <summary>
    /// 尝试修改属性值
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TrySetValue(string name, object? value)
    {
        if (!_members.TryGetValue(name, out var member))
            return false;
        var value0 = member.GetValue(_instance);
        if(CheckChange(value0, value))
        {
            SetValue(name, value);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 复制被修改的属性值
    /// </summary>
    /// <param name="original"></param>
    public void CopyChangedValues(TStructuralType original)
    {
        foreach (var item in _data)
        {
            if (_members.TryGetValue(item.Key, out var member))
                member.SetValue(original, item.Value);
        }
    }
    /// <summary>
    /// 修补
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public TStructuralType Patch(TStructuralType original)
    {
        CopyChangedValues(original);
        return original;
    }
    /// <summary>
    /// 覆盖
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public TStructuralType Put(TStructuralType original)
    {
        foreach (var item in _members)
        {
            if (_data.TryGetValue(item.Key, out var value))
                item.Value.SetValue(original, value);
            else
                item.Value.Copy(_instance, original);
        }
        return original;
    }
    #endregion
}
