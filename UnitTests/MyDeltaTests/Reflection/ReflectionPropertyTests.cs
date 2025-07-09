using MyDeltas.Members;
using MyDeltas.Reflection;
using MyDeltaTests.Supports;
using System.Reflection;

namespace MyDeltaTests.Reflection;

public class ReflectionPropertyTests
{
    const string _propertyName = nameof(TodoItem.Name);
    private ReflectionProperty _reflectionProperty = new();
    private PropertyInfo _property = typeof(TodoItem).GetProperty(_propertyName)!;
    [Fact]
    public void Create()
    {
        var accessor = _reflectionProperty.Create<TodoItem>(_property);
        Assert.IsType<PropertyAccessor<TodoItem>>(accessor);
    }
    [Fact]
    public void CheckMembers()
    {
        PropertyInfo[] properties = [_property];
        Dictionary<string, IMemberAccessor<TodoItem>> members = [];
        _reflectionProperty.CheckMembers(properties, members);
        Assert.True(members.Count == 1);
        Assert.True(members.ContainsKey(_propertyName));
    }
    [Fact]
    public void GetProperties()
    {
        var properties = ReflectionProperty.GetProperties<TodoItem>()
            .ToDictionary(p => p.Name);
        Assert.True(properties.ContainsKey(_propertyName));
    }
}
