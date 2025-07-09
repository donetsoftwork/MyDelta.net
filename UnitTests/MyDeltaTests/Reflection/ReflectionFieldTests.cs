using MyDeltas.Members;
using MyDeltas.Reflection;
using MyDeltaTests.Supports;
using System.Reflection;

namespace MyDeltaTests.Reflection;

public class ReflectionFieldTests
{
    const string _fieldName = nameof(TodoItem.IsComplete);
    private ReflectionField _reflectionField = new();
    private FieldInfo _field = typeof(TodoItem).GetField(_fieldName)!;
    [Fact]
    public void Create()
    {
        var accessor = _reflectionField.Create<TodoItem>(_field);
        Assert.IsType<FieldAccessor<TodoItem>>(accessor);
    }

    [Fact]
    public void CheckMembers()
    {
        FieldInfo[] fields = [_field];
        Dictionary<string, IMemberAccessor<TodoItem>> members = [];
        _reflectionField.CheckMembers(fields, members);
        Assert.True(members.Count == 1);
        Assert.True(members.ContainsKey(_fieldName));
    }
    [Fact]
    public void GetFields()
    {
        var fields = ReflectionField.GetFields<TodoItem>()
            .ToDictionary(p => p.Name);
        Assert.True(fields.ContainsKey(_fieldName));
    }
}
