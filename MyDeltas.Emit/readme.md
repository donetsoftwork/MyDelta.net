# MyDeltas.Emit
>* Emit加速数据变化处理

## 1. 使用EmitDeltaFactory
>代替MyDeltaFactory
~~~csharp
IMyDeltaFactory factory = new EmitDeltaFactory();
~~~

## 2. 数据变化
~~~csharp
MyDelta<TodoItem> delta = factory.Create<TodoItem>();
delta.TrySetValue("Name", "Test");
~~~

## 3. 变化应用到实体
~~~csharp
TodoItem todo = new();
delta.Patch(todo);
~~~

## 4. 支持System.Text.Json序列化
>* 可以配置MVC的JsonSerializerOptions
>* 也可以通过JsonSerializer的JsonSerializerOptions参数直接传入配置

~~~csharp
string json = JsonSerializer.Serialize(delta, new JsonSerializerOptions
{
    WriteIndented = true,
    Converters =
    {
        new MyDeltaConverterFactory(deltaFactory)
    }
});
~~~