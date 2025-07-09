using MyDeltas;
using MyDeltas.Members;
using MyDeltaTests.Supports;

namespace MyDeltaTests.Members;

public class DelegateBuilderTests
{
    private readonly DelegateBuilder<TodoItem> _builder = new();
    public DelegateBuilderTests()
    {
        _builder.Add(nameof(TodoItem.Id), obj => obj.Id, (obj, value) => obj.Id = value)
          .Add(nameof(TodoItem.Name), obj => obj.Name, (obj, value) => obj.Name = value)
          .Add(nameof(TodoItem.IsComplete), obj => obj.IsComplete, (obj, value) => obj.IsComplete = value)
          .Add(nameof(TodoItem.Remark), obj => obj.Remark, (obj, value) => obj.Remark = value);
    }
    [Fact]
    public void Add()
    {
        var members = _builder.Members;
        Assert.True(members.ContainsKey(nameof(TodoItem.Id)));
        Assert.True(members.ContainsKey(nameof(TodoItem.Name)));
        Assert.True(members.ContainsKey(nameof(TodoItem.IsComplete)));
        Assert.True(members.ContainsKey(nameof(TodoItem.Remark)));
    }
    [Fact]
    public void Create()
    {
        TodoItem todo = new() { Id = 1, Name = "Test1" };
        var myDelta = _builder.Create(todo);
        Assert.NotNull(myDelta);
        Assert.False(myDelta.TrySetValue(nameof(TodoItem.Id), 1L));
        Assert.True(myDelta.TrySetValue(nameof(TodoItem.Name), "todo1"));
    }
}
