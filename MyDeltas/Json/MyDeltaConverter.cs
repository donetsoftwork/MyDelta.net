using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyDeltas.Json;

/// <summary>
/// MyDelta转换器
/// </summary>
public class MyDeltaConverter : JsonConverter<MyDelta>
{
    private MyDeltaConverter() { }
    /// <summary>
    /// 实例
    /// </summary>
    public static readonly MyDeltaConverter Instance = new();
    /// <summary>
    /// 读取MyDelta对象(反序列化)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override MyDelta? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Read(ref reader, options);
    /// <summary>
    /// 写入MyDelta对象(序列化)
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, MyDelta value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, value.Data, options);
    /// <summary>
    /// 读取MyDelta对象(反序列化)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static MyDelta? Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, object?>>(ref reader, options);
        if (data is null)
            return null;
        return new MyDelta(data);
    }
    /// <summary>
    /// 读取MyDelta对象(反序列化)
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static MyDelta? Read(ref Utf8JsonReader reader)
        => Read(ref reader, JsonSerializerOptions.Default);
}
