using BenchmarkDotNet.Attributes;
using MyDeltaBench.Supports;
using MyDeltas.Members;
using PocoEmit;

namespace MyDeltaBench.Members;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 200000000)]
public class SetIntBench : MemberBenchBase
{
    IMemberAccessor<TestClass> _propertyId = _property["Id"];
    static Func<TestClass, object?> _emitGetter = Poco.Global.GetReadFunc<TestClass, object?>("Id");
    static Action<TestClass, object?> _emitSetter = Poco.Global.GetWriteAction<TestClass, object?>("Id");
    IMemberAccessor<TestClass> _emitId = new DelegateAccessor<TestClass>(typeof(int), _emitGetter, _emitSetter);
    IMemberAccessor<TestClass> _hardCodeId = new DelegateAccessor<TestClass>(typeof(int),
        obj => obj.Id,
        (obj, value) => obj.Id = (int)value!);


    [Benchmark(Baseline = true)]
    public void HardCode()
    {
        _hardCodeId.SetValue(_testClass, 2);
    }

    [Benchmark]
    public void Emit()
    {
        _emitId.SetValue(_testClass, 2);
    }

    [Benchmark]
    public void Property()
    {
        _propertyId.SetValue(_testClass, 2);
    }
}
