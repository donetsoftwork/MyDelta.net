using MyDeltas.Members;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDeltas.Reflection;

/// <summary>
/// 获取字段成员
/// </summary>
public sealed class ReflectionField : ReflectionMember<FieldInfo>
{
    /// <inheritdoc />
    public override IMemberAccessor<TInstance> Create<TInstance>(FieldInfo member)
        => new FieldAccessor<TInstance>(member);
    #region GetFields
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <returns></returns>
    public static IEnumerable<FieldInfo> GetFields<TInstance>()
        => GetFields(typeof(TInstance));
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<FieldInfo> GetFields(Type type)
    {
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField);
        foreach (var field in fields)
        {
            if (field.IsInitOnly || field.IsLiteral)
                continue; // 忽略只读字段
            yield return field;
        }
    }
    #endregion
}