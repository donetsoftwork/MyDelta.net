using MyDeltas.Members;
using MyDeltaTests.Supports;
using System.Reflection;

namespace MyDeltaTests.Members;

public class PropertyAccessorTests
{
    const string _propertyName = nameof(TodoItem.Name);
    private static readonly PropertyInfo _property = typeof(TodoItem).GetProperty(_propertyName)!;
    private static readonly PropertyAccessor<TodoItem> _accessor = new(_property);

    [Fact]
    public void Property()
    {        
        Assert.Equal(_accessor.Property, _property);
    }
    [Fact]
    public void GetValue()
    {
        TodoItem todo = new() { Name = "Test1" };
        Assert.Equal(todo.Name, _accessor.GetValue(todo));
        TodoItem todo2 = new() { Name = "Test2" };
        Assert.Equal(todo2.Name, _accessor.GetValue(todo2));
    }
    [Fact]
    public void SetValue()
    {
        TodoItem todo = new();
        var value = "Test1";
        _accessor.SetValue(todo, value);
        Assert.Equal(todo.Name, value);
        var value2 = "Test2";
        _accessor.SetValue(todo, value2);
        Assert.Equal(todo.Name, value2);
    }
    [Fact]
    public void Copy()
    {
        var value = "todo";
        TodoItem todo = new() { Name = value };
        var value2 = "todo2";
        TodoItem todo2 = new() { Name = value2 };
        Assert.Equal(todo2.Name, value2);
        _accessor.Copy(todo, todo2);
        Assert.Equal(todo2.Name, value);
    }
}
