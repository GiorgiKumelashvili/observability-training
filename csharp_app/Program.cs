using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Prometheus metrics
builder.Services.AddMetrics();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

// Use metrics middleware
app.UseHttpMetrics();
app.UseMetricServer();

// Health check endpoint
app.MapGet("/health", () => Results.Ok("ok"));

// Results.Content("Hello World!", "text/html")
app.MapGet("/", () => Results.File(Path.Combine(app.Environment.ContentRootPath, "assets/index.html"), "text/html"));

ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Hello world");
logger.LogDebug("debug");
logger.LogError("error");
logger.LogCritical("critical");
logger.LogTrace("trace");
logger.LogWarning("warning");

Console.WriteLine("hello world 2");
// Optional: expose an endpoint that returns some metrics (if needed)
// but the /metrics endpoint is already exposed by UseMetricServer()

app.Run();