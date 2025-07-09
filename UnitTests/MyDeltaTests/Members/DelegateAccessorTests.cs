using MyDeltas.Members;
using MyDeltaTests.Supports;

namespace MyDeltaTests.Members;

public class DelegateAccessorTests
{
    private static readonly Func<TodoItem, long> _getter = todo => todo.Id;
    private static readonly Action<TodoItem, long> _setter = (todo, value) => todo.Id = value;
    private static readonly DelegateAccessor<TodoItem, long> _accessor = new(_getter, _setter);

    [Fact]
    public void Delegate()
    {
        Assert.Equal(_getter, _accessor.Getter);
        Assert.Equal(_setter, _accessor.Setter);
    }
    [Fact]
    public void GetValue()
    {
        TodoItem todo = new() { Id = 1 };
        Assert.Equal(todo.Id, _accessor.GetValue(todo));
        TodoItem todo2 = new() { Id = 2 };
        Assert.Equal(todo2.Id, _accessor.GetValue(todo2));
    }
    [Fact]
    public void SetValue()
    {
        TodoItem todo = new();
        var value = 1L;
        _accessor.SetValue(todo, value);
        Assert.Equal(todo.Id, value);
        var value2 = 2L;
        _accessor.SetValue(todo, value2);
        Assert.Equal(todo.Id, value2);
    }
    [Fact]
    public void Copy()
    {
        var value = 1;
        TodoItem todo = new() { Id = value };
        var value2 = 2;
        TodoItem todo2 = new() { Id = value2 };
        Assert.Equal(todo2.Id, value2);
        _accessor.Copy(todo, todo2);
        Assert.Equal(todo2.Id, value);
    }
}
