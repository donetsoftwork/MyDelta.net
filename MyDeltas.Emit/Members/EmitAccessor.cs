using MyDeltas.Emit.Reflection;
using MyDeltas.Members;
using PocoEmit;
using PocoEmit.Members;
using System;

namespace MyDeltas.Emit.Members;

/// <summary>
/// Emit成员访问器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <param name="reader"></param>
/// <param name="writer"></param>
public class EmitAccessor<TInstance>(IEmitMemberReader reader, IEmitMemberWriter writer)
    : MyDeltas.Members.MemberAccessor<TInstance>(writer.ValueType), IMemberAccessor<TInstance>
{
    #region 配置
    private readonly IEmitMemberReader _reader = reader;
    private readonly IEmitMemberWriter _writer = writer;
    private readonly Func<TInstance, object> _readFunc = EmitHelper.CompileReadFunc<TInstance>(reader);
    private readonly Action<TInstance, object?> _writeAction = EmitHelper.CompileWriteAction<TInstance>(writer);
    private readonly Action<TInstance, TInstance> _copyAction = EmitHelper.CompileCopyAction<TInstance>(reader, writer);
    /// <summary>
    /// 成员读取器
    /// </summary>
    public IEmitMemberReader Reader 
        => _reader;
    /// <summary>
    /// 成员写入器
    /// </summary>
    public IEmitMemberWriter Writer 
        => _writer;
    /// <summary>
    /// 成员读取委托
    /// </summary>
    public Func<TInstance, object> ReadFunc 
        => _readFunc;
    /// <summary>
    /// 成员写入委托
    /// </summary>
    public Action<TInstance, object?> WriteAction
        => _writeAction;
    /// <summary>
    /// 成员复制委托
    /// </summary>
    public Action<TInstance, TInstance> CopyAction 
        => _copyAction;
    #endregion
    #region MemberAccessor
    /// <inheritdoc />
    public override object GetValue(TInstance instance)
        => _readFunc(instance);
    /// <inheritdoc />
    protected override void SetValueCore(TInstance instance, object? value)
        => _writeAction(instance, value);
    /// <inheritdoc />
    public override void Copy(TInstance from, TInstance to)
        => _copyAction(from, to);
    #endregion
}
