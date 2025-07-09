using MyDeltas.Emit.Reflection;
using MyDeltas.EmitTests.Supports;
using MyDeltas.Members;
using System.Reflection;

namespace MyDeltas.EmitTests.Reflection;

public class EmitPropertyTests
{
    const string _propertyName = nameof(TodoItem.Name);
    private EmitProperty _emitProperty = new();
    private PropertyInfo _property = typeof(TodoItem).GetProperty(_propertyName)!;
    [Fact]
    public void Create()
    {
        var accessor = _emitProperty.Create<TodoItem>(_property);
        Assert.IsType<DelegateAccessor<TodoItem>>(accessor);
    }
    [Fact]
    public void CheckMembers()
    {
        PropertyInfo[] properties = [_property];
        Dictionary<string, IMemberAccessor<TodoItem>> members = [];
        _emitProperty.CheckMembers(properties, members);
        Assert.True(members.Count == 1);
        Assert.True(members.ContainsKey(_propertyName));
    }
}
