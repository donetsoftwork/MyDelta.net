# MyDeltas.OpenApi
>* MyDeltas的ApiSchema转化器
>* 让OpenApi文档与实际效果一致

## 1. TransformMyDelta
## 1.1 配置TransformMyDelta
~~~json
services.AddOpenApi(options => options.TransformMyDelta());
~~~

## 1.2 转化前
~~~json
"MyDeltaOfTodoItem": { }
~~~

## 1.3 使用TransformMyDelta后
>转化后与被处理的原类型一致
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