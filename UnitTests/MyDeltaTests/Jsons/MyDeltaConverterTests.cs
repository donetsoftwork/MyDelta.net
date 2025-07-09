using MyDeltas;
using MyDeltas.Json;
using System.Text;
using System.Text.Json;

namespace MyDeltaTests.Jsons;

public class MyDeltaConverterTests
{
    MyDeltaConverter _converter = MyDeltaConverter.Instance;
    string _json = """
        {
            "id": 3,
            "name": "Task 3",
            "isComplete": false,
            "remark": "Three task"
        }
        """;

    [Fact]
    public void CanConvert()
    {
        Assert.True(_converter.CanConvert(typeof(MyDelta)));
    }
    [Fact]
    public void Read()
    {
        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(_json));
        JsonSerializerOptions options = new();
        var myDelta = _converter.Read(ref reader, typeof(MyDelta), options);
        AssertMyDelta(myDelta);
    }
    [Fact]
    public void Read0()
    {
        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(_json));
        JsonSerializerOptions options = new();
        var myDelta = MyDeltaConverter.Read(ref reader, options);
        AssertMyDelta(myDelta);
    }
    [Fact]
    public void Read2()
    {
        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(_json));
        var myDelta = MyDeltaConverter.Read(ref reader);
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
        MyDelta myDelta = new(data);

        JsonWriterOptions writerOptions = new() { Indented = true };
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream, writerOptions);
        _converter.Write(writer, myDelta, JsonSerializerOptions.Default);
        string json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.NotEmpty(json);
        foreach (var key in data.Keys)
        {
            Assert.Contains(key, _json);
        }        
    }

    private void AssertMyDelta(MyDelta? myDelta)
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
