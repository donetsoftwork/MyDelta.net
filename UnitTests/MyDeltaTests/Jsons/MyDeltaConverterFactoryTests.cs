using MyDeltas;
using MyDeltas.Json;
using MyDeltaTests.Supports;
using System;
using System.Text;
using System.Text.Json;

namespace MyDeltaTests.Jsons;

public class MyDeltaConverterFactoryTests
{
    private static readonly MyDeltaFactory _factory = new();
    private static readonly MyDeltaConverterFactory _converter = new();
    private readonly JsonSerializerOptions _options = new();
    public MyDeltaConverterFactoryTests()
    {
        _options.Converters.Add(_converter);
    }

    [Fact]
    public void CanConvert()
    {
        Assert.True(_converter.CanConvert(typeof(MyDelta)));
        Assert.True(_converter.CanConvert(typeof(MyDelta<TodoItem>)));
        Assert.False(_converter.CanConvert(typeof(TodoItem)));
    }
    [Fact]
    public void Read()
    {
        ReadOnlySpan<byte> json = """
        {
            "id": 3,
            "name": "Task 3",
            "isComplete": false,
            "remark": "Three task"
        }
        """u8;
        Utf8JsonReader reader = new(json);
        var myDelta = JsonSerializer.Deserialize<MyDelta<TodoItem>>(ref reader, _options);
        AssertMyDelta(myDelta);
    }
    [Fact]
    public void Write()
    {
        var data = new Dictionary<string, object?> {
            { "id", 3 },
            { "name", "Task 3" },
            { "isComplete", false },
            { "remark", "Three task" }
        };
        var myDelta = _factory.Create<TodoItem>(data);

        JsonWriterOptions writerOptions = new() { Indented = true };
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream, writerOptions);
        JsonSerializer.Serialize(writer, myDelta, _options);
        string json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.NotEmpty(json);
        foreach (var key in data.Keys)
        {
            Assert.Contains(key, json);
        }
    }

    private static void AssertMyDelta(MyDelta? myDelta)
    {
        Assert.NotNull(myDelta);
        var data = myDelta.Data;
        Assert.NotNull(data);
        Assert.True(data.ContainsKey("id"));
        Assert.True(data.ContainsKey("name"));
        Assert.True(data.ContainsKey("isComplete"));
        Assert.True(data.ContainsKey("remark"));
    }
}
