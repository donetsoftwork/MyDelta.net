using PocoEmit.Members;
using System;
using System.Linq.Expressions;

namespace MyDeltas.Emit.Reflection;

/// <summary>
/// Emit帮助类
/// </summary>
internal static class EmitHelper
{
    /// <summary>
    /// 编译读取委托
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="reader"></param>
    /// <returns></returns>
    internal static Func<TInstance, object> CompileReadFunc<TInstance>(IEmitMemberReader reader)
    {
        var instance = Expression.Parameter(typeof(TInstance), "instance");
        var valueType = typeof(object);
        var body = reader.Read(instance);
        if(reader.ValueType != valueType)
            body = Expression.Convert(body, valueType);
        var lambda = Expression.Lambda<Func<TInstance, object>>(body, instance);
        return lambda.Compile();
    }
    /// <summary>
    /// 编译写入委托
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="writer"></param>
    /// <returns></returns>
    internal static Action<TInstance, object?> CompileWriteAction<TInstance>(IEmitMemberWriter writer)
    {
        var instance = Expression.Parameter(typeof(TInstance), "instance");
        var valueType = typeof(object);
        var value = Expression.Parameter(valueType, "value");
        var body = writer.Write(instance, writer.ValueType == valueType ? value : Expression.Convert(value, writer.ValueType));
        var lambda = Expression.Lambda<Action<TInstance, object?>>(body, instance, value);
        return lambda.Compile();
    }
    /// <summary>
    /// 编译复制委托
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="reader"></param>
    /// <param name="writer"></param>
    /// <returns></returns>
    internal static Action<TInstance, TInstance> CompileCopyAction<TInstance>(IEmitMemberReader reader, IEmitMemberWriter writer)
    {
        var type = typeof(TInstance);
        var source = Expression.Parameter(type, "source");
        var dest = Expression.Parameter(type, "dest");
        var body = writer.Write(dest, reader.Read(source));
        var lambda = Expression.Lambda<Action<TInstance, TInstance>>(body, source, dest);
        return lambda.Compile();
    }
}
