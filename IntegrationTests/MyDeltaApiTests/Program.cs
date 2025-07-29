using MyDeltas;
using MyDeltas.Emit;
using MyDeltas.Json;
using PocoEmit;
using Scalar.AspNetCore;

var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseAuthorization();

app.MapControllers();

app.Run();


static void ConfigureServices(IServiceCollection services)
{
    // Add services to the container.
    //Poco.Global
    IMyDeltaFactory deltaFactory = new EmitDeltaFactory(Poco.Global, StringComparer.OrdinalIgnoreCase);
    services.AddSingleton(deltaFactory)
        //.Configure<JsonOptions>(options => options.JsonSerializerOptions.Converters.Add(new MyDeltaConverterFactory(deltaFactory)))
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new MyDeltaConverterFactory(deltaFactory));
        });

    services.AddOpenApi(options => options.TransformMyDelta());
    //var exporter = new JsonSchemaExporter();
}