using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace MyDeltas.OpenApi;

/// <summary>
/// MyDelta 的 OpenAPI 转换器
/// </summary>
public class MyDeltaApiSchemaTransformer : IOpenApiSchemaTransformer
{
    /// <summary>
    /// 处理MyDelta类型的OpenApiSchema
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if(cancellationToken.IsCancellationRequested)
            return Task.CompletedTask;
        var type = context.JsonTypeInfo.Type;
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(MyDelta<>))
        {
            if (schema.Properties.TryGetValue("instance", out var instanceSchema) && instanceSchema is not null)
                schema.Properties = instanceSchema.Properties;
        }
        else if (type == typeof(MyDelta))
        {
            schema.Properties.Clear();
        }
        return Task.CompletedTask;
    }
    // <inheritdoc />
    Task IOpenApiSchemaTransformer.TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
        => TransformAsync(schema, context, cancellationToken);
}
