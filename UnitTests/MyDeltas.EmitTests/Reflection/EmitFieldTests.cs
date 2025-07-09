using MyDeltas.Emit.Reflection;
using MyDeltas.EmitTests.Supports;
using MyDeltas.Members;
using System.Reflection;


namespace MyDeltas.EmitTests.Reflection;

public class EmitFieldTests
{
    const string _fieldName = nameof(TodoItem.IsComplete);
    private EmitField _emitField = new();
    private FieldInfo _field = typeof(TodoItem).GetField(_fieldName)!;
    [Fact]
    public void Create()
    {
        var accessor = _emitField.Create<TodoItem>(_field);
        Assert.IsType<DelegateAccessor<TodoItem>>(accessor);
    }

    [Fact]
    public void CheckMembers()
    {
        FieldInfo[] fields = [_field];
        Dictionary<string, IMemberAccessor<TodoItem>> members = [];
        _emitField.CheckMembers(fields, members);
        Assert.True(members.Count == 1);
        Assert.True(members.ContainsKey(_fieldName));
    }
}
