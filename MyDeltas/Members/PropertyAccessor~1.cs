using System.Reflection;

namespace MyDeltas.Members;

/// <summary>
/// 属性访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <param name="property"></param>
public class PropertyAccessor<TInstance>(PropertyInfo property)
    : MemberAccessor<TInstance>(property.PropertyType)
{
    #region 配置
    private readonly PropertyInfo _property = property;
    /// <summary>
    /// 属性信息
    /// </summary>
    public PropertyInfo Property
        => _property;
    #endregion
    #region 方法
    /// <inheritdoc />
    public override object? GetValue(TInstance instance)
        => _property.GetValue(instance);    
    /// <inheritdoc />
    protected override void SetValueCore(TInstance instance, object? value)
        => _property.SetValue(instance, value);
    #endregion
}
