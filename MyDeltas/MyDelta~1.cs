using MyDeltas.Json;
using MyDeltas.Members;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyDeltas;

/// <summary>
/// 数据变化
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="instance"></param>
/// <param name="members"></param>
/// <param name="changed"></param>
[JsonConverter(typeof(MyDeltaConverterFactory))]
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
    /// <returns>是否修改</returns>
    public bool TrySetValue(string name, object? value)
    {
        if (!_members.TryGetValue(name, out var member))
            return false;
        if(member.CheckChange(_instance, value))
        {
            SetValue(name, value);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 增量修改
    /// </summary>
    /// <param name="original"></param>
    /// <returns>是否变化</returns>
    public bool Patch(TStructuralType original)
    {
        bool changed = false;
        foreach (var item in _data)
        {
            var key = item.Key;
            if (_members.TryGetValue(key, out var member))
            {
                var value = item.Value;
                var valueChecked = member.CheckValue(value);
                changed = member.TrySetValue(original, valueChecked);
                if (CheckChange(value, valueChecked))
                    _data[key] = valueChecked;
            }      
        }
        return changed;
    }
    /// <summary>
    /// 覆盖
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public void Put(TStructuralType original)
    {
        foreach (var item in _members)
        {
            if (_data.TryGetValue(item.Key, out var value))
                item.Value.SetValue(original, value);
            else
                item.Value.Copy(_instance, original);
        }
    }
    #endregion
}
