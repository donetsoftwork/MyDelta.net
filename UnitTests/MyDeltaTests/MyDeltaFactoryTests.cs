using MyDeltas;
using MyDeltas.Members;
using MyDeltaTests.Supports;
using System.Text.Json;

namespace MyDeltaTests;

public class MyDeltaFactoryTests
{
    private static readonly MyDeltaFactory _factory = new();
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
        Assert.IsType<FieldAccessor<TodoItem>>(isCompleteAccessor);
        Assert.True(members.TryGetValue(nameof(TodoItem.Remark), out var remarkAccessor));
        Assert.IsType<PropertyAccessor<TodoItem>>(remarkAccessor);
    }
    [Fact]
    public void Create()
    {
        MyDelta<TodoItem> delta = _factory.Create<TodoItem>();
        Assert.True(delta.Data.Count == 0);
        delta.SetValue(nameof(TodoItem.Name), "Change2");
        Assert.True(delta.HasChanged("Name"));
        Assert.True(delta.Data.Count == 1);
    }
    [Fact]
    public void Change()
    {
        var todo = new TodoItem { Id = 1, Name = "Task 1", IsComplete = false, Remark = "First task" };
        MyDelta<TodoItem> delta1 = _factory.Create(todo);
        Assert.True(delta1.Data.Count == 0);
        delta1.SetValue(nameof(TodoItem.Name), "Change");
        var todoNew = new TodoItem();
        delta1.Put(todoNew);
        Assert.False(delta1.Patch(todoNew));
        delta1.SetValue(nameof(TodoItem.IsComplete), true);
        Assert.True(delta1.Patch(todoNew));
    }
    [Fact]
    public void Json()
    {
        MyDelta<TodoItem> delta = _factory.Create<TodoItem>();
        delta.TrySetValue(nameof(TodoItem.Name), "Test");
        string json = JsonSerializer.Serialize(delta);
        Assert.Contains("Name", json);
    }
}
