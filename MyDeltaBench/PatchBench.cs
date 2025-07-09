using BenchmarkDotNet.Attributes;
using MyDeltaBench.Supports;
using MyDeltas;
using MyDeltas.Emit;
using MyDeltas.Members;

namespace MyDeltaBench;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 20000000)]
public class PatchBench
{
    public PatchBench()
    {
        _default = InitMyDelta(new MyDeltaFactory());
        _emit = InitMyDelta(new EmitDeltaFactory());
        var builder = new DelegateBuilder<TestClass>()
            .Add(nameof(TestClass.Id), obj => obj.Id, (obj, value) => obj.Id = value)
            .Add(nameof(TestClass.Name), obj => obj.Name, (obj, value) => obj.Name = value)
            .Add(nameof(TestClass.CreatedAt), obj => obj.CreatedAt, (obj, value) => obj.CreatedAt = value)
            .Add(nameof(TestClass.IntField), obj => obj.IntField, (obj, value) => obj.IntField = value)
            .Add(nameof(TestClass.StringField), obj => obj.StringField, (obj, value) => obj.StringField = value)
            .Add(nameof(TestClass.StringProperty), obj => obj.StringProperty, (obj, value) => obj.StringProperty = value)
            .Add(nameof(TestClass.DateTimeField), obj => obj.DateTimeField, (obj, value) => obj.DateTimeField = value);
        _hardCode = builder.Create();
        InitMyDelta(_hardCode);
    }
    private MyDelta<TestClass> InitMyDelta(IMyDeltaFactory factory)
    {
        var myDelta = factory.Create<TestClass>();
        InitMyDelta(myDelta);
        return myDelta;
    }
    private void InitMyDelta(MyDelta<TestClass> myDelta)
    {
        myDelta.SetValue(nameof(TestClass.Name), "DeltaBench");
        myDelta.SetValue(nameof(TestClass.Id), 11);
        myDelta.SetValue(nameof(TestClass.CreatedAt), DateTime.Now);
        //myDelta.SetValue(nameof(TestClass.IntField), 12);
        //myDelta.SetValue(nameof(TestClass.StringField), "StringField");
        //myDelta.SetValue(nameof(TestClass.StringProperty), "StringProperty");
        //myDelta.SetValue(nameof(TestClass.DateTimeField), DateTime.Now);
    }
    MyDelta<TestClass> _default;
    MyDelta<TestClass> _emit;
    MyDelta<TestClass> _hardCode;
    TestClass _test = new();
    [Benchmark(Baseline = true)]
    public void Default()
    {
        _default.CopyChangedValues(_test);
    }
    //[Benchmark]
    //public void CopyChangedValues2()
    //{
    //    _default.CopyChangedValues2(_test);
    //}
    [Benchmark]
    public void Emit()
    {
        _emit.Patch(_test);
    }
    [Benchmark]
    public void HardCode()
    {
        _hardCode.Patch(_test);
    }
}
