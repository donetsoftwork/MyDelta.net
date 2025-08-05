using System.Reflection;

namespace MyDeltas.Members;

/// <summary>
/// 字段访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <param name="field"></param>
public class FieldAccessor<TInstance>(FieldInfo field)
    : MemberAccessor<TInstance>(field.FieldType)
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
    public override object? GetValue(TInstance instance)
        => _field.GetValue(instance);
    /// <inheritdoc />
    protected override void SetValueCore(TInstance instance, object? value)
        => _field.SetValue(instance, value);
    #endregion
}
