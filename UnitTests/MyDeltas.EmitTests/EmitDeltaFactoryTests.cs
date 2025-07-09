using MyDeltas.Emit;
using MyDeltas.EmitTests.Supports;
using MyDeltas.Members;

namespace MyDeltas.EmitTests;

public class EmitDeltaFactoryTests
{
    private static readonly EmitDeltaFactory _factory = new();

    [Fact]
    public void GetMembers()
    {
        var members = _factory.GetMembers<TodoItem>();
        Assert.True(members.ContainsKey(nameof(TodoItem.Name)));
    }
    [Fact]
    public void Use()
    {
        var builder = new DelegateBuilder<TodoItem>()
          .Add(nameof(TodoItem.Id), obj => obj.Id, (obj, value) => obj.Id = value)
          .Add(nameof(TodoItem.Name), obj => obj.Name, (obj, value) => obj.Name = value);
        var factory = _factory.Use(builder.Members);
        var members = factory.GetMembers<TodoItem>();
        Assert.True(members.ContainsKey(nameof(TodoItem.Name)));
        Assert.True(members.TryGetValue(nameof(TodoItem.Id), out var idAccessor));
        Assert.IsType<DelegateAccessor<TodoItem, long>>(idAccessor);
        Assert.True(members.TryGetValue(nameof(TodoItem.IsComplete), out var isCompleteAccessor));
        Assert.IsType<DelegateAccessor<TodoItem>>(isCompleteAccessor);
        Assert.True(members.TryGetValue(nameof(TodoItem.Remark), out var remarkAccessor));
        Assert.IsType<DelegateAccessor<TodoItem>>(remarkAccessor);
    }
    [Fact]
    public void Create()
    {
        MyDelta<TodoItem> delta = _factory.Create<TodoItem>();
        Assert.True(delta.Data.Count == 0);
        delta.SetValue("Name", "Change2");
        Assert.True(delta.HasChanged("Name"));
        Assert.True(delta.Data.Count == 1);
    }
}
