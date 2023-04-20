var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// --- DI

// --- Services

// --- Configs


app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => new { Message = "Hello World" });

app.Run();
