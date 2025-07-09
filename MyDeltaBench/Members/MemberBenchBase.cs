using MyDeltaBench.Supports;
using MyDeltas;
using MyDeltas.Members;

namespace MyDeltaBench.Members;

public abstract class MemberBenchBase
{
    protected TestClass _testClass = new()
    {
        Id = 1,
        Name = "Test",
        CreatedAt = DateTime.Now
    };
    private static MyDeltaFactory _defaultMemberAccessorFactory = new();
    protected static IDictionary<string, IMemberAccessor<TestClass>> _property = _defaultMemberAccessorFactory.GetMembers<TestClass>();
}
