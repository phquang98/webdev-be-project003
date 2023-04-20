using simple_api_program.Controllers;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// --- DI

// --- Services

// --- Configs

// --- Minimal API

app.MapGet("/", () => "Hello World!");
app.MapGet("/healthcheck", () => MiniAPI.HealthChecking());
app.MapGet(
    "/test/{jsonPlaceHolderIdHere}",
    (int jsonPlaceHolderIdHere) => MiniAPI.GetOneUserById(jsonPlaceHolderIdHere)
);

app.Run();
