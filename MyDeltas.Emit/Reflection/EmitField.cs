using MyDeltas.Members;
using MyDeltas.Reflection;
using PocoEmit;
using System.Reflection;

namespace MyDeltas.Emit.Reflection;

/// <summary>
/// 获取字段成员
/// </summary>
public sealed class EmitField : ReflectionMember<FieldInfo>
{
    /// <inheritdoc />
    public override IMemberAccessor<TStructuralType> Create<TStructuralType>(FieldInfo member)
        => new DelegateAccessor<TStructuralType>(member.FieldType, InstanceFieldHelper.EmitGetter<TStructuralType, object?>(member), InstanceFieldHelper.EmitSetter<TStructuralType, object?>(member));
}