using System;

namespace MyDeltas.Members;

/// <summary>
/// 委托访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <param name="memberType"></param>
/// <param name="getter"></param>
/// <param name="setter"></param>
public class DelegateAccessor<TInstance>(Type memberType, Func<TInstance, object?> getter, Action<TInstance, object?> setter)
     : DelegateAccessor<TInstance, object?>(memberType, getter, setter)
{
}
