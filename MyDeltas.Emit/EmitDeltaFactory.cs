using MyDeltas.Members;
using PocoEmit;
using PocoEmit.Collections;
using System;
using System.Collections.Generic;

namespace MyDeltas;

/// <summary>
/// Emit成员访问器工厂
/// </summary>
/// <param name="options"></param>
/// <param name="memberComparer"></param>
public class EmitDeltaFactory(IPoco options, IEqualityComparer<string> memberComparer)
    :  MemberAccessorFactoryBase(memberComparer)
{
    /// <summary>
    /// 默认成员访问器工厂
    /// </summary>
    public EmitDeltaFactory()
        : this(Poco.Default, StringComparer.Ordinal)
    {
    }
    #region 配置
    private readonly IPoco _options = options;
    /// <summary>
    /// Emit配置
    /// </summary>
    public IPoco Options
        => _options;
    #endregion

    /// <inheritdoc />
    protected override void CheckMembers<TInstance>(IDictionary<string, IMemberAccessor<TInstance>> members)
    {
        var bundle = _options.GetTypeMembers<TInstance>();
        if (bundle == null)
            return;
        var readerCacher = MemberContainer.Instance.MemberReaderCacher;
        foreach (var writer in bundle.EmitWriters.Values)
        {
            var member = writer.Info;
            var reader = readerCacher.Get(member);
            if(reader is null)
                continue;
            EmitAccessor<TInstance> accessor = new(reader, writer);
            members[member.Name] = accessor;
        }
    }
}
