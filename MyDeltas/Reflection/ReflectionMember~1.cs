using MyDeltas.Members;
using System.Collections.Generic;
using System.Reflection;

namespace MyDeltas.Reflection;

/// <summary>
/// 获取成员
/// </summary>
/// <typeparam name="TMember"></typeparam>
public abstract class ReflectionMember<TMember>
    where TMember : MemberInfo
{
    ////100M内存,用来测试延迟初始化
    //byte[] _test = new byte[99_999_999];
    /// <summary>
    /// 补充成员
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="list"></param>
    /// <param name="members"></param>
    public void CheckMembers<TStructuralType>(IEnumerable<TMember> list, IDictionary<string, IMemberAccessor<TStructuralType>> members)
    {
        foreach (var item in list)
        {
            var memberName = item.Name;
            members.TryGetValue(memberName, out var member);
            if (member is null)
                members[memberName] = Create<TStructuralType>(item);
        }
    }
    /// <summary>
    /// 构造新成员
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="member"></param>
    /// <returns></returns>
    public abstract IMemberAccessor<TStructuralType> Create<TStructuralType>(TMember member);
}
