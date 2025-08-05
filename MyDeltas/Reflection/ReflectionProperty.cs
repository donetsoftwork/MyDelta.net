using MyDeltas.Members;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDeltas.Reflection;

/// <summary>
/// 获取属性成员
/// </summary>
public sealed class ReflectionProperty : ReflectionMember<PropertyInfo>
{
    /// <inheritdoc />
    public override IMemberAccessor<TInstance> Create<TInstance>(PropertyInfo member)
        => new PropertyAccessor<TInstance>(member);
    #region GetProperties
    /// <summary>
    /// 获取所有属性
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <returns></returns>
    public static IEnumerable<PropertyInfo> GetProperties<TInstance>()
        => GetProperties(typeof(TInstance));
    /// <summary>
    /// 获取所有属性
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty);
        foreach (var property in properties)
        {
            if (property.CanWrite && property.CanWrite)
                yield return property;
        }
    }
    #endregion
}
