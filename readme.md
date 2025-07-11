# MyDeltas
>* 数据变化,类似OData中的Delta

## 1. 配置MyDeltaFactory
~~~csharp
IMyDeltaFactory factory = new MyDeltaFactory();
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
>* 不配置默认使用MyDeltaFactory
### 4.1  配置MVC的JsonSerializerOptions
~~~csharp
services.AddSingleton(deltaFactory)
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new MyDeltaConverterFactory(deltaFactory));
    });
~~~

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
