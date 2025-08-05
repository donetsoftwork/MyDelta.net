using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyDeltas.Json;

/// <summary>
/// 泛型MyDelta转换器
/// </summary>
/// <typeparam name="TInstance"></typeparam>
/// <param name="deltaFactory"></param>
public class MyDeltaConverter<TInstance>(IMyDeltaFactory deltaFactory)
    : JsonConverter<MyDelta<TInstance>>
{
    /// <summary>
    /// 泛型MyDelta转换器
    /// </summary>
    public MyDeltaConverter()
        : this(new MyDeltaFactory())
    {
    }
    private readonly IMyDeltaFactory _deltaFactory = deltaFactory;
    /// <summary>
    /// 读取MyDelta对象(反序列化)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override MyDelta<TInstance>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, object?>>(ref reader, options);
        if (data is null)
            return null;
        return _deltaFactory.Create<TInstance>(data);
    }
    /// <summary>
    /// 写入MyDelta对象(序列化)
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, MyDelta<TInstance> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Data, options);
    }
}
