using BenchmarkDotNet.Attributes;
using MyDeltaBench.Supports;
using MyDeltas.Members;
using PocoEmit;

namespace MyDeltaBench.Members;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 200000000)]
public class GetStringBench : MemberBenchBase
{
    IMemberAccessor<TestClass> _propertyName = _property["Name"];
    static Func<TestClass, object?> _emitGetter = Poco.Global.GetReadFunc<TestClass, object?>("Name");
    static Action<TestClass, object?> _emitSetter = Poco.Global.GetWriteAction<TestClass, object?>("Name");
    IMemberAccessor<TestClass> _emitName = new DelegateAccessor<TestClass>(typeof(string), _emitGetter, _emitSetter);
    IMemberAccessor<TestClass> _hardCodeName = new DelegateAccessor<TestClass>(typeof(string),
        obj => obj.Name,
        (obj, value) => obj.Name = (string)value!);


    [Benchmark(Baseline = true)]
    public object? HardCode()
    {
        return _hardCodeName.GetValue(_testClass);
    }

    [Benchmark]
    public object? Emit()
    {
        return _emitName.GetValue(_testClass);
    }
    [Benchmark]
    public object? Property()
    {
        return _propertyName.GetValue(_testClass);
    }
}
