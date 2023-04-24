using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using simple_api_program.Controllers;

var builder = WebApplication.CreateBuilder(args);

// --- DI

// --- Services

// Services must be registered to be used later in app
//builder.Services.AddAuthorization();

//builder.Services.AddMvc();  // not needed as using endpoints routing already in minimal API, also deprecated
//builder.Services.AddControllers();    // allows ctrl to access services have been registered in the app DI
//example, allow this to happen:   private readonly IVatTuDB _iVatTuDB;public VatTuController(IVatTuDB iVatTuDB){_iVatTuDB = iVatTuDB;}

//builder.Services.AddCors(opts =>
//{

//});

builder.Services.AddEndpointsApiExplorer(); // required if use minimal API
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Simple App",
            Description = "An ASP.NET Core Minimal API for reference",
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://creativecommons.org/licenses/by-sa/4.0/")
            }
        }
    );
    opts.SwaggerDoc(
        "v3",
        new OpenApiInfo { Version = "v2", Title = "Simple App V2 Newer and Better", }
    );
}); // from Swashbuckle.AspNetCore

builder.Services.AddHealthChecks();

// --- Configs

var app = builder.Build();

// --- Middlewares
//app.UseRouting(); // matching incoming reqs to the corresponding endpoint, always top, default in minimal API
//app.UseMvc(); // not needed as using endpoints routing already in minimal API, also deprecated
//app.UseCors();  // placed after UseRouting, but before UseAuthorization and MapControllers
//app.MapEndpoints();   // used to add custom mdwr logic to endpoints
//app.UseEndpoints(endpoints =>
//{
//    // Here, define all avail endpoints accessable to any path
//    endpoints.MapControllers();
//});   // after matching, send incoming reqs to appropriate endpoint hdlr, redudance in minimal API as already directly send reqs to designated endpoints under MapGet, ...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // generate a JSON file, typically at swagger/v1/swagger.json
    app.UseSwaggerUI(swaggerUIOpts =>
    {
        swaggerUIOpts.SwaggerEndpoint("/swagger/v1/swagger.json", "Newer API I choose you!"); // config to choose what version of Swagger JSON
    }); // generate swagger/index.html
}
app.MapHealthChecks("/healthz").RequireHost("*:9002");

//app.UseAuthorization();

// --- Minimal API

app.MapGet("/", () => "Hello World!");
app.MapGet("/customhealthcheck", () => MiniAPI.HealthChecking());
//app.MapGet("/special-healthcheck", () => MiniAPI.HealthChecking()).RequireAuthorization();
app.MapGet(
    "/test/{jsonPlaceHolderIdHere}",
    (int jsonPlaceHolderIdHere) => MiniAPI.GetOneUserById(jsonPlaceHolderIdHere)
);
app.MapGet("/result", () => MiniAPI.CustomResult());
app.MapGet("/async", async () => await MiniAPI.CustomAsync());


// --- Start

app.Run();
