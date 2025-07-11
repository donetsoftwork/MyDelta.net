using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyDeltas.Json;

/// <summary>
/// MyDelta转换器工厂
/// </summary>
/// <param name="deltaFactory"></param>
public class MyDeltaConverterFactory(IMyDeltaFactory deltaFactory)
    : JsonConverterFactory
{
    /// <summary>
    /// MyDelta转换器工厂
    /// </summary>
    public MyDeltaConverterFactory()
        : this(new MyDeltaFactory())
    {
        //var stackTrace = new System.Diagnostics.StackTrace();
        //Console.WriteLine(stackTrace);
    }
    private readonly IMyDeltaFactory _deltaFactory = deltaFactory;
    /// <summary>
    /// 类型是否可以转换
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if(typeToConvert.IsGenericType)
            return typeToConvert.GetGenericTypeDefinition() == typeof(MyDelta<>);
        return typeToConvert == typeof(MyDelta);
    }
    /// <summary>
    /// 构造转换器
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert.IsGenericType)
        {
            Type elementType = typeToConvert.GetGenericArguments()[0];
            var converterType = typeof(MyDeltaConverter<>).MakeGenericType(elementType);
            var converter = Activator.CreateInstance(converterType, _deltaFactory);
            return converter as JsonConverter;
        }
        return MyDeltaConverter.Instance;
    }
}
