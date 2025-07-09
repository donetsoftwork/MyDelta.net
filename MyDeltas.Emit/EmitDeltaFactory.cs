using MyDeltas.Emit.Reflection;
using MyDeltas.Members;
using MyDeltas.Reflection;
using System;
using System.Collections.Generic;

namespace MyDeltas.Emit;

/// <summary>
/// 默认成员访问器工厂
/// </summary>
/// <param name="memberComparer"></param>
public class EmitDeltaFactory(IEqualityComparer<string> memberComparer)
    :  MemberAccessorFactoryBase(memberComparer)
{
    /// <summary>
    /// 默认成员访问器工厂
    /// </summary>
    public EmitDeltaFactory()
        : this(StringComparer.Ordinal)
    {
    }
    /// <inheritdoc />
    protected override void CheckMembers<TStructuralType>(IDictionary<string, IMemberAccessor<TStructuralType>> members)
    {
        Inner.Property.CheckMembers(ReflectionProperty.GetProperties<TStructuralType>(), members);
        Inner.Field.CheckMembers(ReflectionField.GetFields<TStructuralType>(), members);
    }
     /// <summary>
    /// 内部延迟初始化
    /// </summary>
    static class Inner
    {
        /// <summary>
        /// 获取属性成员
        /// </summary>
        public static readonly EmitProperty Property = new();
        /// <summary>
        /// 获取字段成员
        /// </summary>
        public static readonly EmitField Field = new();
    }
}
