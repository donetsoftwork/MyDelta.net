# MyDeltas.OpenApi
>* ApiSchema转化器
>* 让OpenApi文档与实际效果一致

## 1. TransformMyDelta
>在OpenApi文档去掉多余部分
~~~csharp
services.AddOpenApi(options => options.TransformMyDelta());
~~~

## 1.1 转化前
~~~json
"MyDeltaOfTodoItem": {
  "type": "object",
  "properties": {
    "instance": {
      "$ref": "#/components/schemas/TodoItem2"
    },
    "data": {
      "type": "object",
      "nullable": true
    }
  }
}
~~~

## 1.2 使用TransformMyDelta后
>转化后与被差异处理的原类型一致
~~~json
"MyDeltaOfTodoItem": {
  "type": "object",
  "properties": {
    "id": {
      "type": "integer",
      "format": "int64"
    },
    "name": {
      "type": "string",
      "nullable": true
    },
    "isComplete": {
      "type": "boolean"
    },
    "remark": {
      "type": "string",
      "nullable": true
    }
  }
}
~~~