using BenchmarkDotNet.Running;
using MyDeltaBench;
using MyDeltaBench.Members;
using MyDeltas;
using MyDeltas.Reflection;
using PocoEmit;

//TestEmitProperty();
//Delay();

//BenchmarkRunner.Run<GetStringBench>();
//BenchmarkRunner.Run<SetIntBench>();
//new PatchBench().Default();
BenchmarkRunner.Run<PatchBench>();
//BenchmarkRunner.Run<PutBench>();

partial class Program 
{
    static void TestEmitProperty()
    {
        var testClass2 = new TestClass2 
        {
            Id = 1,
            Name = "InitialName",
            CreatedAt = DateTime.Now
        };
        Action<TestClass2, object?> nameSetter = InstancePropertyHelper.EmitSetter<TestClass2, object?>("Name");
        nameSetter(testClass2, "EmitName");
        Console.WriteLine(testClass2.Name);
        Func<TestClass2, object?> idGetter = InstancePropertyHelper.EmitGetter<TestClass2, object?>("Id");
        Console.WriteLine(idGetter(testClass2));
    }

    static void Delay()
    {
        int i = 0;
        Console.WriteLine(i++);
        Console.WriteLine("观察内存变化");
        Console.ReadLine();
        ReflectionProperty property = new();
        Console.WriteLine(i++);
        Console.WriteLine("内存增加");
        Console.ReadLine();
        var fields = ReflectionField.GetFields<TestClass2>();
        Console.WriteLine(i++);
        Console.WriteLine("内存不变");
        Console.ReadLine();
        var properties = ReflectionProperty.GetProperties<TestClass2>();
        Console.WriteLine(i++);
        Console.WriteLine("内存不变");
        Console.ReadLine();
        MyDeltaFactory factory = new();
        Console.WriteLine(i++);
        Console.WriteLine("内存不变");
        Console.ReadLine();
        var members = factory.GetMembers<TestClass2>();
        Console.WriteLine(i++);
        Console.WriteLine("内存增加");
        Console.ReadLine();
        Console.WriteLine(members.Count);
    }

    class TestClass2
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
