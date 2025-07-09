using System.Reflection;

namespace MyDeltas.Members;

/// <summary>
/// 字段访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="field"></param>
public class FieldAccessor<TStructuralType>(FieldInfo field)
    : MemberAccessor<TStructuralType>(field.FieldType)
{
    #region 配置
    private readonly FieldInfo _field = field;
    /// <summary>
    /// 字段信息
    /// </summary>
    public FieldInfo Field
        => _field;
    #endregion
    #region 方法
    /// <inheritdoc />
    public override object? GetValue(TStructuralType instance)
        => _field.GetValue(instance);
    /// <inheritdoc />
    protected override void SetValueCore(TStructuralType instance, object? value)
        => _field.SetValue(instance, value);
    #endregion
}
