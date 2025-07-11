# MyDeltas.Emit
>* Emit加速数据变化处理

## 1. 使用EmitDeltaFactory
>代替MyDeltaFactory
~~~csharp
IMyDeltaFactory emitFactory = new EmitDeltaFactory();
~~~

## 2. 数据变化
~~~csharp
MyDelta<TodoItem> delta = emitFactory.Create<TodoItem>();
delta.TrySetValue("Name", "Test");
~~~

## 3. 变化应用到实体
~~~csharp
TodoItem todo = new();
bool changed = delta.Patch(todo);
~~~

## 4. 支持System.Text.Json序列化
### 4.1  配置MVC的JsonSerializerOptions
~~~csharp
services.AddSingleton(emitFactory)
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new MyDeltaConverterFactory(emitFactory));
    });
~~~

### 4.2  通过JsonSerializer的JsonSerializerOptions参数直接传入配置
~~~csharp
MyDelta<TodoItem> delta = emitFactory.Create<TodoItem>();
delta.TrySetValue("Name", "Test");
string json = JsonSerializer.Serialize(delta, new JsonSerializerOptions
{
    Converters =
    {
        new MyDeltaConverterFactory(emitFactory)
    }
});
//{"Name":"Test"}
~~~

### 4.3 不配置默认使用MyDeltaFactory
