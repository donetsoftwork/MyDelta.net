using Microsoft.AspNetCore.OpenApi;
using MyDeltas.OpenApi;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 扩展方法，用于在服务集合中添加MyDelta的OpenApi转换器。
/// </summary>
public static class MyDeltaOpenApiServiceCollectionExtensions
{
    /// <summary>
    /// 增加MyDelta的OpenApi转换器
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static OpenApiOptions TransformMyDelta(this OpenApiOptions options)
    {
        var transformer = new MyDeltaApiSchemaTransformer(/*options*/);
        return options.AddSchemaTransformer(transformer)
            .AddDocumentTransformer(transformer);
    }
}
