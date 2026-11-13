var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");
var app = builder.Build();

app.MapGet("/health", () => Results.Text("OK\n"));
app.MapGet("/log-test", (ILogger<Program> log) =>
{
    log.LogInformation("Log test info");
    log.LogWarning("Log test warning");
    log.LogError("Log test error");
    return Results.Text("logged\n");
});
app.MapGet("/demo", () =>
{
    var greeting = Environment.GetEnvironmentVariable("SAMPLE_GREETING");
    return Results.Content(DemoPage.Render(string.IsNullOrEmpty(greeting) ? "Hello" : greeting), "text/html; charset=utf-8");
});
app.MapGet("/", () =>
{
    // This is a deliberately public greeting, not a diagnostics/environment dump.
    var greeting = Environment.GetEnvironmentVariable("SAMPLE_GREETING");
    return Results.Text($"App Service Builder / dotnet-web: {(string.IsNullOrEmpty(greeting) ? "Hello" : greeting)}\n");
});

app.Run();
