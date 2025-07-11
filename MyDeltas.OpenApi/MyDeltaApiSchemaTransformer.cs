using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace MyDeltas.OpenApi;

/// <summary>
/// MyDelta 的 OpenAPI 转换器
/// </summary>
public class MyDeltaApiSchemaTransformer/*(OpenApiOptions options)*/
    : IOpenApiSchemaTransformer, IOpenApiDocumentTransformer/*, IOpenApiOperationTransformer*/
{
    //private const string SchemaId = "x-schema-id";
    //private readonly OpenApiOptions _options = options;
    private OpenApiDocument? _document = null;
    private readonly Dictionary<Type, OpenApiSchema> _schemas = [];
    // <inheritdoc />
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if(cancellationToken.IsCancellationRequested)
            return Task.CompletedTask;
        var jsonTypeInfo = context.JsonTypeInfo;
        var type = jsonTypeInfo.Type;
        if (type.IsGenericType)
        {
            if (type.GetGenericTypeDefinition() == typeof(MyDelta<>))
            {
                var instanceType = type.GenericTypeArguments[0];
                if (_schemas.TryGetValue(instanceType, out var instanceSchema))
                {
                    schema.Properties = instanceSchema.Properties;
                }
                else if(_document?.Components?.Schemas is IDictionary<string, OpenApiSchema> schemas && schemas.TryGetValue(instanceType.Name, out instanceSchema))
                {
                    schema.Properties = instanceSchema.Properties;
                }
                //else
                //{
                //    var instanceJsonTypeInfo = JsonTypeInfo.CreateJsonTypeInfo(instanceType, JsonSerializerOptions.Default);
                //    var instanceSchemaId = _options.CreateSchemaReferenceId(instanceJsonTypeInfo);
                //    schema.Annotations[SchemaId] = instanceSchemaId;
                //} 
            }
        }
        else if (!_schemas.ContainsKey(type))
        {
            _schemas[type] = schema;
        }
        return Task.CompletedTask;
    }
    //// <inheritdoc />
    //public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    //{
    //    //context.GetOrCreateSchemaAsync
    //    throw new NotImplementedException();
    //}
    // <inheritdoc />
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        _document = document;
        return Task.CompletedTask;
    }
}
