using System;

namespace MyDeltas.Members;

/// <summary>
/// 成员访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
public abstract class MemberAccessor<TStructuralType>(Type memberType)
    : IMemberAccessor<TStructuralType>
{
    private readonly Type _memberType = memberType;
    /// <summary>
    /// 成员类型
    /// </summary>
    public Type MemberType
        => _memberType;
    /// <summary>
    /// 复制值
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public virtual void Copy(TStructuralType from, TStructuralType to)
        => SetValueCore(to, GetValue(from));
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public abstract object? GetValue(TStructuralType instance);
    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    public void SetValue(TStructuralType instance, object? value)
        => SetValueCore(instance, MyDelta.CheckValueType(value, _memberType));
    /// <summary>
    /// 设置值原始方法
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    protected abstract void SetValueCore(TStructuralType instance, object? value);
}
