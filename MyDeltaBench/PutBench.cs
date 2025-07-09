using BenchmarkDotNet.Attributes;
using MyDeltaBench.Supports;
using MyDeltas;
using MyDeltas.Emit;
using MyDeltas.Members;

namespace MyDeltaBench;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 20000000)]
public class PutBench
{
    public PutBench()
    {
        _default = InitMyDelta(new MyDeltaFactory());
        _emit = InitMyDelta(new EmitDeltaFactory());
        var builder = new DelegateBuilder<TestClass>()
            .Add(nameof(TestClass.Id), obj => obj.Id, (obj, value) => obj.Id = value)
            .Add(nameof(TestClass.Name), obj => obj.Name, (obj, value) => obj.Name = value)
            .Add(nameof(TestClass.CreatedAt), obj => obj.CreatedAt, (obj, value) => obj.CreatedAt = value);
        _hardCode = builder.Create(new TestClass() { Id = 1, Name = "MyDelta", CreatedAt = DateTime.Now });
        InitMyDelta(_hardCode);
    }
    private static MyDelta<TestClass> InitMyDelta(IMyDeltaFactory factory)
    {
        var myDelta = factory.Create(new TestClass() { Id = 1, Name = "MyDelta", CreatedAt = DateTime.Now });
        InitMyDelta(myDelta);
        return myDelta;
    }
    private static void InitMyDelta(MyDelta<TestClass> myDelta)
    {
        myDelta.SetValue(nameof(TestClass.Name), "DeltaBench");
    }
    MyDelta<TestClass> _default;
    MyDelta<TestClass> _emit;
    MyDelta<TestClass> _hardCode;
    TestClass _test = new();
    [Benchmark(Baseline = true)]
    public void Default()
    {
        _default.Put(_test);
    }
    //[Benchmark]
    //public void Put2()
    //{
    //    _default.Put2(_test);
    //}
    [Benchmark]
    public void Emit()
    {
        _emit.Put(_test);
    }
    [Benchmark]
    public void HardCode()
    {
        _hardCode.Put(_test);
    }
}
