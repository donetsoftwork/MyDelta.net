using System;

namespace MyDeltas.Members;

/// <summary>
/// 委托访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="memberType"></param>
/// <param name="getter"></param>
/// <param name="setter"></param>
public class DelegateAccessor<TStructuralType>(Type memberType, Func<TStructuralType, object?> getter, Action<TStructuralType, object?> setter)
     : DelegateAccessor<TStructuralType, object?>(memberType, getter, setter)
{
}
