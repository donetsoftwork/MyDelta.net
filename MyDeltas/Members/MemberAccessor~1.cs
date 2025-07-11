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
    /// <inheritdoc />
    public virtual void Copy(TStructuralType from, TStructuralType to)
        => SetValueCore(to, GetValue(from));
    /// <inheritdoc />
    public abstract object? GetValue(TStructuralType instance);
    /// <inheritdoc />
    public bool TrySetValue(TStructuralType instance, object? value)
    {
        var valueChecked = CheckValue(value);
        if (MyDelta.CheckChange(GetValue(instance), valueChecked))
        {
            SetValueCore(instance, valueChecked);
            return true;
        }
        return false;
    }
    /// <inheritdoc />
    public object? CheckValue(object? value)
        => MyDelta.CheckValueType(value, _memberType);
    /// <inheritdoc />
    public bool CheckChange(TStructuralType instance, object? value)
        => MyDelta.CheckChange(GetValue(instance), CheckValue(value));
    /// <inheritdoc />
    public void SetValue(TStructuralType instance, object? value)
        => SetValueCore(instance, CheckValue(value));
    /// <summary>
    /// 设置值原始方法
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    protected abstract void SetValueCore(TStructuralType instance, object? value);
}
