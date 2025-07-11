using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyDeltaApiTests.Models;
using MyDeltas;
using System.Net.Mime;

namespace MyDeltaApiTests.Controllers;

/// <summary>
/// 代办事项
/// </summary>
[Route("[controller]")]
[ApiController]
public class TodoController(IOptions<JsonOptions> jsonOptions) : ControllerBase
{
    private static List<TodoItem> _todoItems = [];
    private readonly IOptions<JsonOptions> _jsonOptions = jsonOptions;

    static TodoController()
    {
        // 初始化一些示例数据
        _todoItems.Add(new TodoItem { Id = 1, Name = "Task 1", IsComplete = false, Remark = "First task" });
        _todoItems.Add(new TodoItem { Id = 2, Name = "Task 2", IsComplete = true, Remark = "Second task" });
    }
    /// <summary>
    /// 获取代办列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    //[ProducesResponseType<IEnumerable<TodoItem>>(200)]
    public ActionResult<IEnumerable<TodoItem>> Get()
    {
        return Ok(_todoItems);
    }
    /// <summary>
    /// 获取代办
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType<TodoItem>(200)]
    [ProducesResponseType<string>(404)]
    public ActionResult GetById([FromRoute] long id)
    {
        var existingTodo = _todoItems.FirstOrDefault(t => t.Id == id);
        if (existingTodo == null)
            return NotFound($"Todo with Id {id} not found.");
        return Ok(existingTodo);
    }
    /// <summary>
    /// 添加代办
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType<TodoItem>(200)]
    [ProducesResponseType<string>(400)]
    public ActionResult Put([FromBody] TodoItem todo)
    {
        var name = todo.Name;
        if (string.IsNullOrEmpty(name))
            return BadRequest("Name cannot be null or empty.");
        var id = todo.Id;
        if (id <= 0)
            return BadRequest("Id must be greater than zero.");
        var existingTodo = _todoItems.FirstOrDefault(t => t.Id == id || name.Equals(t.Name));
        if (existingTodo != null)
            return BadRequest($"Todo with Id {id} or Name '{name}' already exists.");
        _todoItems.Add(todo);
        return Ok(todo);
    }
    /// <summary>
    /// 修改代办
    /// </summary>
    /// <param name="id"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 保存代办的变化
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="id"></param>
    /// <param name="changed"></param>
    /// <returns></returns>
    [HttpPatch("PatchChanged{id}")]
    [Consumes(typeof(TodoItem), MediaTypeNames.Application.Json)]
    [ProducesResponseType<TodoItem>(200)]
    [ProducesResponseType<string>(404)]
    public ActionResult PatchChanged([FromServices] IMyDeltaFactory factory, [FromRoute] long id, [FromBody] IDictionary<string, object?> changed)
    {
        var existingTodo = _todoItems.FirstOrDefault(t => t.Id == id);
        if (existingTodo == null)
            return NotFound($"Todo with Id {id} not found.");
        var delta = factory.Create(existingTodo, changed);
        // 应用变化
        delta.Patch(existingTodo);
        return Ok(existingTodo);
    }
    /// <summary>
    /// 保存代办的变化
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="id"></param>
    /// <param name="myDelta"></param>
    /// <returns></returns>
    [HttpPatch("PatchMyDelta{id}")]
    [Consumes(typeof(TodoItem), MediaTypeNames.Application.Json)]
    [ProducesResponseType<TodoItem>(200)]
    [ProducesResponseType<string>(404)]
    public ActionResult PatchMyDelta([FromServices] IMyDeltaFactory factory, [FromRoute] long id, [FromBody] MyDelta myDelta)
    {
        var existingTodo = _todoItems.FirstOrDefault(t => t.Id == id);
        if (existingTodo == null)
            return NotFound($"Todo with Id {id} not found.");
        var delta = factory.Create(existingTodo, myDelta);
        // 应用变化
        delta.Patch(existingTodo);
        return Ok(existingTodo);
    }
    /// <summary>
    /// 删除代办
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType<TodoItem>(200)]
    [ProducesResponseType<string>(404)]
    public ActionResult Delete([FromRoute] long id)
    {
        var todo = _todoItems.FirstOrDefault(t => t.Id == id);
        if (todo == null)
            return NotFound($"Todo with Id {id} not found.");
        _todoItems.Remove(todo);
        return Ok(todo);
    }
}
