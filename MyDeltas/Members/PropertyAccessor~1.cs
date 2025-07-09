using System.Reflection;

namespace MyDeltas.Members;

/// <summary>
/// 属性访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="property"></param>
public class PropertyAccessor<TStructuralType>(PropertyInfo property)
    : MemberAccessor<TStructuralType>(property.PropertyType)
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
    public override object? GetValue(TStructuralType instance)
        => _property.GetValue(instance);    
    /// <inheritdoc />
    protected override void SetValueCore(TStructuralType instance, object? value)
        => _property.SetValue(instance, value);
    #endregion
}
