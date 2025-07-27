using MyDeltas.Emit.Members;
using MyDeltas.Members;
using PocoEmit;
using PocoEmit.Collections;
using PocoEmit.Configuration;
using System;
using System.Collections.Generic;

namespace MyDeltas.Emit;

/// <summary>
/// Emit成员访问器工厂
/// </summary>
/// <param name="options"></param>
/// <param name="memberComparer"></param>
public class EmitDeltaFactory(IPocoOptions options, IEqualityComparer<string> memberComparer)
    :  MemberAccessorFactoryBase(memberComparer)
{
    /// <summary>
    /// 默认成员访问器工厂
    /// </summary>
    public EmitDeltaFactory()
        : this(Poco.Global, StringComparer.Ordinal)
    {
    }
    #region 配置
    private readonly IPocoOptions _options = options;
    /// <summary>
    /// Emit配置
    /// </summary>
    public IPocoOptions Options
        => _options;
    #endregion

    /// <inheritdoc />
    protected override void CheckMembers<TStructuralType>(IDictionary<string, IMemberAccessor<TStructuralType>> members)
    {
        var list = _options.GetTypeMembers<TStructuralType>()?.WriteMembers?.Values;
        if (list == null || list.Count == 0)
            return;
        var readerCacher = MemberContainer.Instance.MemberReaderCacher;
        var writerCacher = MemberContainer.Instance.MemberWriterCacher;
        foreach (var item in list)
        {
            var reader = readerCacher.Get(item);
            if(reader is null)
                continue;
            var writer = writerCacher.Get(item);
            if (writer is null)
                continue;
            EmitAccessor<TStructuralType> accessor = new(reader, writer);
            members[item.Name] = accessor;
        }
    }
}
