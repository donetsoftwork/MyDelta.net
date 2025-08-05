using System;

namespace MyDeltas.Members;

/// <summary>
/// 成员访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
public abstract class MemberAccessor<TInstance>(Type memberType)
    : IMemberAccessor<TInstance>
{
    private readonly Type _memberType = memberType;
    /// <summary>
    /// 成员类型
    /// </summary>
    public Type MemberType
        => _memberType;
    /// <inheritdoc />
    public virtual void Copy(TInstance from, TInstance to)
        => SetValueCore(to, GetValue(from));
    /// <inheritdoc />
    public abstract object? GetValue(TInstance instance);
    /// <inheritdoc />
    public bool TrySetValue(TInstance instance, object? value)
    {
        if (MyDelta.CheckChange(GetValue(instance), value))
        {
            SetValueCore(instance, value);
            return true;
        }
        return false;
    }
    /// <inheritdoc />
    public object? CheckValue(object? value)
        => MyDelta.CheckValueType(value, _memberType);

    /// <inheritdoc />
    public bool CheckChange(TInstance instance, object? value)
        => MyDelta.CheckChange(GetValue(instance), CheckValue(value));
    /// <inheritdoc />
    public void SetValue(TInstance instance, object? value)
        => SetValueCore(instance, CheckValue(value));
    /// <summary>
    /// 设置值原始方法
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    protected abstract void SetValueCore(TInstance instance, object? value);
}
