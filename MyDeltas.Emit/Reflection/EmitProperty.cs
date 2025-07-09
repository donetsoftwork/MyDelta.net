using MyDeltas.Members;
using MyDeltas.Reflection;
using PocoEmit;
using System.Reflection;

namespace MyDeltas.Emit.Reflection;

/// <summary>
/// 获取属性成员
/// </summary>
public sealed class EmitProperty : ReflectionMember<PropertyInfo>
{
    /// <inheritdoc />
    public override IMemberAccessor<TStructuralType> Create<TStructuralType>(PropertyInfo member)
        => new DelegateAccessor<TStructuralType>(member.PropertyType, InstancePropertyHelper.EmitGetter<TStructuralType, object?>(member), InstancePropertyHelper.EmitSetter<TStructuralType, object?>(member));
}
