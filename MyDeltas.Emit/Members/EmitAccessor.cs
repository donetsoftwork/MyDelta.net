using MyDeltas.Emit.Reflection;
using MyDeltas.Members;
using PocoEmit;
using PocoEmit.Members;
using System;

namespace MyDeltas.Emit.Members;

/// <summary>
/// Emit成员访问器
/// </summary>
/// <typeparam name="TStructuralType"></typeparam>
/// <param name="reader"></param>
/// <param name="writer"></param>
public class EmitAccessor<TStructuralType>(IEmitMemberReader reader, IEmitMemberWriter writer)
    : MyDeltas.Members.MemberAccessor<TStructuralType>(writer.ValueType), IMemberAccessor<TStructuralType>
{
    #region 配置
    private readonly IEmitMemberReader _reader = reader;
    private readonly IEmitMemberWriter _writer = writer;
    private readonly Func<TStructuralType, object> _readFunc = EmitHelper.CompileReadFunc<TStructuralType>(reader);
    private readonly Action<TStructuralType, object?> _writeAction = EmitHelper.CompileWriteAction<TStructuralType>(writer);
    private readonly Action<TStructuralType, TStructuralType> _copyAction = EmitHelper.CompileCopyAction<TStructuralType>(reader, writer);
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
    public Func<TStructuralType, object> ReadFunc 
        => _readFunc;
    /// <summary>
    /// 成员写入委托
    /// </summary>
    public Action<TStructuralType, object?> WriteAction
        => _writeAction;
    /// <summary>
    /// 成员复制委托
    /// </summary>
    public Action<TStructuralType, TStructuralType> CopyAction 
        => _copyAction;
    #endregion
    #region MemberAccessor
    /// <inheritdoc />
    public override object GetValue(TStructuralType instance)
        => _readFunc(instance);
    /// <inheritdoc />
    protected override void SetValueCore(TStructuralType instance, object? value)
        => _writeAction(instance, value);
    /// <inheritdoc />
    public override void Copy(TStructuralType from, TStructuralType to)
        => _copyAction(from, to);
    #endregion
}
