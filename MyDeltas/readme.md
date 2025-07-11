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
bool changed = delta.Patch(todo);
~~~

## 4. 支持System.Text.Json序列化
### 4.1 直接序列化 
~~~csharp
MyDelta<TodoItem> delta = factory.Create<TodoItem>();
delta.TrySetValue("Name", "Test");
string json = JsonSerializer.Serialize(delta);
//{"Name":"Test"}
~~~

### 4.2 作为Mvc参数接收部分字段修改
>支持Patch请求
~~~csharp
[HttpPatch("{id}")]
[ProducesResponseType<TodoItem>(200)]
[ProducesResponseType<string>(304)]
[ProducesResponseType<string>(404)]
public ActionResult Patch([FromRoute] long id, [FromBody] MyDelta<TodoItem> delta)
{
    var existingTodo = _todoItems.FirstOrDefault(t => t.Id == id);
    if (existingTodo == null)
        return NotFound($"Todo with Id {id} not found.");
    // 应用变化
    if (delta.Patch(existingTodo))
        return Ok(existingTodo);
    return StatusCode(304, "Todo with Id {id} not modified.");
}
~~~
