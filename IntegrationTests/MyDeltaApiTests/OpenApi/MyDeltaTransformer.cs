//using Microsoft.AspNetCore.OpenApi;
//using Microsoft.OpenApi.Extensions;
//using Microsoft.OpenApi.Models;
//using MyDeltas;

//namespace MyDeltaApiTests.OpenApi;

///// <summary>
///// MyDelta处理器
///// </summary>
//public class MyDeltaTransformer
//    : IOpenApiSchemaTransformer
//    , IOpenApiOperationTransformer
//    , IOpenApiDocumentTransformer
//{
//    const string SchemaKey = "x-schema-id";
//    private OpenApiDocument? _document = null;
//    private IDictionary<string, OpenApiSchema> _schemas = new Dictionary<string, OpenApiSchema>();
//    //private Task _documentTask = Task.CompletedTask;
//    TaskCompletionSource _documentCompletion = new();
//    //private readonly OpenApiSchemaService _componentService;
//    ///// <summary>
//    ///// 
//    ///// </summary>
//    ///// <param name="documentTask"></param>
//    //public void WaitDocument(Task documentTask)
//    //{
//    //    _documentTask = documentTask;
//    //}
//    /// <summary>
//    /// 
//    /// </summary>
//    //public IDictionary<string, OpenApiSchema> Schemas
//    //    => _document?.Components?.Schemas ?? _schemas;
//    private readonly HashSet<string> _schemaIds = [];
//    private IDictionary<string, string> _mapSchema = new Dictionary<string, string>();

//    private OpenApiSchema MapElementType(string schemaId, Type elementType)
//    {
//        if (_schemas.TryGetValue(schemaId, out var schema))
//            return schema;
//        var elementName = elementType.Name;
//        if (!_schemas.TryGetValue(elementName, out schema))
//        {
//            lock (_schemas)
//            {
//                if (!_schemas.TryGetValue(elementName, out schema))
//                {
//                    schema = OpenApiTypeMapper.MapTypeToOpenApiPrimitiveType(elementType);
//                    var annotations = schema.Annotations ??= new Dictionary<string, object>();
//                    annotations[SchemaKey] = elementName;
//                    _schemas[elementName] = schema;
//                }
//            }
//        }
//        //_schemas[schemaId] = schema;
//        //_mapSchema[schemaId] = elementName;
//        _schemas[schemaId] = schema;
//        return schema;
//    }
//    ///// <summary>
//    ///// 收集MyDelta类型的OpenApiSchema
//    ///// </summary>
//    ///// <param name="schema"></param>
//    ///// <param name="context"></param>
//    ///// <param name="cancellationToken"></param>
//    ///// <returns></returns>
//    //public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
//    //{
//    //    //await _documentCompletion.Task;
//    //    var typeToConvert = context.JsonTypeInfo.Type;
//    //    if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(MyDelta<>))
//    //    {
//    //        if (schema.Annotations.TryGetValue(SchemaKey, out var schemaId))
//    //        {
//    //            if (schemaId is string key)
//    //            {
//    //                var elementType = typeToConvert.GetGenericArguments()[0];
//    //                var elementSchema = MapElementType(key, elementType);
//    //                _schemas[key] = elementSchema;
//    //                _schemaIds.Add(key);
//    //                _mapSchema[key] = elementType.Name;
//    //            }
//    //        }
//    //    }
//    //    return Task.CompletedTask;
//    //}
//    /// <summary>
//    /// 收集MyDelta类型的OpenApiSchema
//    /// </summary>
//    /// <param name="schema"></param>
//    /// <param name="context"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
//    {
//        //await _documentCompletion.Task;
//        var type = context.JsonTypeInfo.Type;
//        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(MyDelta<>))
//        {
//            if (schema.Properties.TryGetValue("instance", out var instanceSchema) && instanceSchema is not null)
//                schema.Properties = instanceSchema.Properties;
//        }
//        else if (type == typeof(MyDelta))
//        {
//            schema.Properties.Clear();
//        }
//        return Task.CompletedTask;
//    }
//    /// <summary>
//    /// 修改OpenApiOperation
//    /// </summary>
//    /// <param name="operation"></param>
//    /// <param name="context"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
//    {
//        //await _documentCompletion.Task;
//        if (operation.RequestBody is OpenApiRequestBody body)
//        {
//            //if( body.Content.Values.FirstOrDefault() is OpenApiMediaType mediaType)
//            //{
//            //    var parameterSchema = mediaType.Schema;
//            //    //var annotations = parameterSchema.Annotations;
//            //    if (parameterSchema.Annotations.TryGetValue(SchemaKey, out var schemaId))
//            //    {
//            //        if (schemaId is string key && _mapSchema.TryGetValue(key, out var newKey))
//            //        {
//            //            //annotations[SchemaKey] = newKey;
//            //            if (Schemas.TryGetValue(newKey, out var transformedSchema))
//            //                mediaType.Schema = transformedSchema;
//            //        }
//            //    }
//            //}

//            foreach (OpenApiMediaType item in body.Content.Values)
//            {
//                var parameterSchema = item.Schema;
//                //var annotations = parameterSchema.Annotations;
//                if (parameterSchema.Annotations.TryGetValue(SchemaKey, out var schemaId))
//                {
//                    if (schemaId is string key && _mapSchema.TryGetValue(key, out var newKey))
//                    {
//                        //annotations[SchemaKey] = newKey;
//                        if (_schemas.TryGetValue(newKey, out var transformedSchema))
//                            item.Schema = transformedSchema;
//                    }
//                }
//            }
//        }
//        return Task.CompletedTask;
//    }
//    /// <summary>
//    /// 整理文档
//    /// </summary>
//    /// <param name="document"></param>
//    /// <param name="context"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
//    {
//        _document = document;
//        //var components = _document.Components ??= new OpenApiComponents();
//        _schemas = document?.Components?.Schemas ?? new Dictionary<string, OpenApiSchema>();
//        //_documentCompletion.SetResult();
//        return Task.CompletedTask;
//    }

//    private static string? GetSchemaReferenceId(OpenApiSchema schema)
//    {
//        IDictionary<string, object> annotations = schema.Annotations;
//        if (annotations != null && annotations.TryGetValue("x-schema-id", out var value) && value is string result)
//        {
//            return result;
//        }

//        return null;
//    }
//}
