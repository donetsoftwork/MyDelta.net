using MyDeltas;
using System.Text.Json;

namespace MyDeltaTests;

public class MyDeltaTests
{
    [Fact]
    public void Change()
    {
        MyDelta delta = new();
        Assert.True(delta.Data.Count == 0);
        delta.SetValue("Name", "Change");
        Assert.True(delta.HasChanged("Name"));
        Assert.True(delta.Data.Count == 1);
    }
}