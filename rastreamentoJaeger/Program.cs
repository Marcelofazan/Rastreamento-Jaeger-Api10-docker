using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ------------ Health Checks ------------
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("API OK"))
    .AddMongoDb(
        sp =>
        {
            var mongoSection = builder.Configuration.GetSection("MongoDb");
            var cs = mongoSection["ConnectionString"] ?? "mongodb://localhost:27017";

            var mongoUrl = new MongoUrl(cs);
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);

            // Inscreve o assinante de atividades para capturar comandos do MongoDB nos traces
            clientSettings.ClusterConfigurator = cb => cb.Subscribe(new MongoDB.Driver.Core.Extensions.DiagnosticSources.DiagnosticsActivityEventSubscriber());

            return new MongoClient(clientSettings);
        },
        name: "mongodb",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "db", "mongo" }
    );

// ------------ OpenTelemetry (Tracing) ------------
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(ResourceBuilder.CreateDefault()
                .AddService("MinhaApiMongo")) // Nome visível no painel do Jaeger
            .AddAspNetCoreInstrumentation()
            .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources"); // Rastreia o Mongo

        if (builder.Environment.IsEnvironment("Testing"))
        {
            tracing.AddConsoleExporter();
        }
        else
        {
            var jaegerHost = builder.Configuration["Jaeger:Host"] ?? "http://localhost"; // Aceita o prefixo http:// se vier do docker
            var jaegerPort = builder.Configuration["Jaeger:Port"] ?? "4317";

            tracing.AddOtlpExporter(opt =>
            {
                // Se o host já começar com http, não duplicamos o termo
                var baseUri = jaegerHost.StartsWith("http") ? $"{jaegerHost}:{jaegerPort}" : $"http://{jaegerHost}:{jaegerPort}";
                opt.Endpoint = new Uri(baseUri);
                opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
            });
        }
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "Minha API v1");
    });
}

app.UseCors("AllowAll");

// Redireciona requisições HTTP para HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Endpoint de saúde estruturado
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
