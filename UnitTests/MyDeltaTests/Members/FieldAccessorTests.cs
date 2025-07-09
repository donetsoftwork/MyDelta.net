using MyDeltas.Members;
using MyDeltaTests.Supports;
using System.Reflection;

namespace MyDeltaTests.Members;

public class FieldAccessorTests
{
    const string _fieldName = nameof(TodoItem.IsComplete);
    private static readonly FieldInfo _field = typeof(TodoItem).GetField(_fieldName)!;
    private static readonly FieldAccessor<TodoItem> _accessor = new(_field);
    [Fact]
    public void Field()
    {

        Assert.Equal(_accessor.Field, _field);
    }
    [Fact]
    public void GetValue()
    {
        TodoItem todo = new() { Id = 1, IsComplete = true };
        Assert.Equal(todo.IsComplete, _accessor.GetValue(todo));
        TodoItem todo2 = new() { Id = 2, IsComplete = false };
        Assert.Equal(todo2.IsComplete, _accessor.GetValue(todo2));
    }
    [Fact]
    public void SetValue()
    {
        TodoItem todo = new();
        var value = true;
        _accessor.SetValue(todo, value);
        Assert.Equal(todo.IsComplete, value);
        var value2 = false;
        _accessor.SetValue(todo, value2);
        Assert.Equal(todo.IsComplete, value2);
    }
    [Fact]
    public void Copy()
    {
        var value = true;
        TodoItem todo = new() { IsComplete = value };
        var value2 = false;
        TodoItem todo2 = new() { IsComplete = value2 };
        Assert.Equal(todo2.IsComplete, value2);
        _accessor.Copy(todo, todo2);
        Assert.Equal(todo2.IsComplete, value);
    }
}
