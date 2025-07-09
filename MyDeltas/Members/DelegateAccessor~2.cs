using System;

namespace MyDeltas.Members;

/// <summary>
/// 委托访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <typeparam name="TMember"></typeparam>
public class DelegateAccessor<TStructuralType, TMember>
     : MemberAccessor<TStructuralType>
{
    /// <summary>
    /// 委托访问器
    /// </summary>
    /// <param name="getter"></param>
    /// <param name="setter"></param>
    public DelegateAccessor(Func<TStructuralType, TMember> getter, Action<TStructuralType, TMember> setter)
        : this(typeof(TMember), getter, setter)
    {
    }
    /// <summary>
    /// 委托访问器
    /// </summary>
    /// <param name="memberType"></param>
    /// <param name="getter"></param>
    /// <param name="setter"></param>
    protected DelegateAccessor(Type memberType, Func<TStructuralType, TMember> getter, Action<TStructuralType, TMember> setter)
            : base(memberType)
    {
        _getter = getter;
        _setter = setter;
    }
    #region 配置
    private readonly Func<TStructuralType, TMember> _getter;
    private readonly Action<TStructuralType, TMember> _setter;
    /// <summary>
    /// 读取器
    /// </summary>
    public Func<TStructuralType, TMember> Getter
        => _getter;
    /// <summary>
    /// 写入器
    /// </summary>
    public Action<TStructuralType, TMember> Setter
        => _setter;
    #endregion
    #region 方法
    /// <inheritdoc />
    public override object? GetValue(TStructuralType instance)
        => _getter(instance);
    /// <inheritdoc />
    protected override void SetValueCore(TStructuralType instance, object? value)
#pragma warning disable CS8600, CS8604 // 引用类型参数可能为 null。
        => _setter(instance, (TMember)value);
#pragma warning restore CS8600,CS8604 // 引用类型参数可能为 null。
    /// <inheritdoc />
    public override void Copy(TStructuralType from, TStructuralType to)
        => _setter(to, _getter(from));
    #endregion
}
