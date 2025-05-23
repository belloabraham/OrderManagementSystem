/*Entry point for the Order Management System API.
Sets up the application host, services, middleware, and database context.*/

using System.Reflection;
using System.Threading.Channels;
using Microsoft.AspNetCore.Http.Features;
using OrderManagementSystem.API;
using OrderManagementSystem.Application.AutoMapper;
using OrderManagementSystem.Domain.Requests;
using OrderManagementSystem.Infrastructure;
using OrderManagementSystem.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

/*Configures standardized error responses using Problem Details (RFC 7807).
Includes request path and trace identifiers for better debugging.*/
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method}:{context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

/*Registers an unbounded channel for asynchronously processing discount logic in a background service.
The channel ensures one reader and disables synchronous continuations for better async handling.*/
builder.Services.AddSingleton<Channel<DiscountRequest>>(_ => Channel.CreateUnbounded<DiscountRequest>(
    new UnboundedChannelOptions()
    {
        SingleReader = true,
        AllowSynchronousContinuations = false
    }
));

// Binds the ConnectionStrings section from appsettings.json to a strongly-typed configuration class.
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

/*Registers global exception handler middleware.
Handles known and unknown errors consistently.*/
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

/*Registers custom application services (repositories, domain services, validators, background workers, etc.).
Located in OrderManagementSystem.API/ServiceRegistration.cs.*/
builder.Services.RegisterServices();

//Registers AutoMapper and loads mappings defined in MappingProfile.cs.
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();


/*Ensures the database is created (if it doesn't exist).
This is useful for development or initial deployment scenarios.*/
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

/*Activates global exception handling middleware.
Catches unhandled exceptions and formats them using the registered GlobalExceptionHandler.*/
app.UseExceptionHandler();
//Adds basic status code pages (e.g. 404, 500) for non-API browser clients.
app.UseStatusCodePages();

/*Registers Swagger only for development environments.
Swagger UI provides interactive API documentation and test interface.*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*Enables authorization middleware (e.g. for [Authorize] attributes).
 Does not enable authentication (if needed, must be registered separately).*/
app.UseAuthorization();

/*Maps controller endpoints to route table.
Enables route-based API access.*/
app.MapControllers();

// Starts the application and begins listening for HTTP requests.
app.Run();