using System;

namespace MyDeltas.Members;

/// <summary>
/// 委托访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <typeparam name="TMember"></typeparam>
public class DelegateAccessor<TInstance, TMember>
     : MemberAccessor<TInstance>
{
    /// <summary>
    /// 委托访问器
    /// </summary>
    /// <param name="getter"></param>
    /// <param name="setter"></param>
    public DelegateAccessor(Func<TInstance, TMember> getter, Action<TInstance, TMember> setter)
        : this(typeof(TMember), getter, setter)
    {
    }
    /// <summary>
    /// 委托访问器
    /// </summary>
    /// <param name="memberType"></param>
    /// <param name="getter"></param>
    /// <param name="setter"></param>
    protected DelegateAccessor(Type memberType, Func<TInstance, TMember> getter, Action<TInstance, TMember> setter)
            : base(memberType)
    {
        _getter = getter;
        _setter = setter;
    }
    #region 配置
    private readonly Func<TInstance, TMember> _getter;
    private readonly Action<TInstance, TMember> _setter;
    /// <summary>
    /// 读取器
    /// </summary>
    public Func<TInstance, TMember> Getter
        => _getter;
    /// <summary>
    /// 写入器
    /// </summary>
    public Action<TInstance, TMember> Setter
        => _setter;
    #endregion
    #region 方法
    /// <inheritdoc />
    public override object? GetValue(TInstance instance)
        => _getter(instance);
    /// <inheritdoc />
    protected override void SetValueCore(TInstance instance, object? value)
#pragma warning disable CS8600, CS8604 // 引用类型参数可能为 null。
        => _setter(instance, (TMember)value);
#pragma warning restore CS8600,CS8604 // 引用类型参数可能为 null。
    /// <inheritdoc />
    public override void Copy(TInstance from, TInstance to)
        => _setter(to, _getter(from));
    #endregion
}
