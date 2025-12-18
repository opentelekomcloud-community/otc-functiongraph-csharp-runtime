namespace http_minimalWebAPI;

using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Expressions;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Templates;

public class Program
{


  public static void Main(string[] args)
  {

    // Create the WebApplication builder
    var builder = WebApplication.CreateBuilder();

    // timezone conversion functions for Serilog.Expressions
    var dateTimeFunctions = new StaticMemberNameResolver(typeof(DateTimeFunctions));

    // Configure Serilog
    builder.Host.UseSerilog((context, loggerConfig) =>
        loggerConfig
        .WriteTo.Async(a => a.Console(
          new ExpressionTemplate(
            "{ { ts: ToUtc(@t), requestId: XCFFRequestId, lvl: @l,  msg: @m, threadId: ThreadId } }\n",
            nameResolver: dateTimeFunctions
          )
        ), bufferSize: 50)
        .Enrich.FromLogContext()
        .Enrich.WithThreadId()
    );


    // add swagger
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal Web API", Version = "v1" });

    });

    var app = builder.Build();

    // Use the custom logging middleware
    app.UseMiddleware<LoggingMiddleware>();

    // Enable serving static files (including favicon.ico from wwwroot)
    app.UseStaticFiles();

    // Enable Swagger middleware
    app.UseSwagger();
    app.UseSwaggerUI();

    // Map API endpoints
    APIEndpoints.Map(app);

    // Start the application
    app.Run();
  }

  public static class APIEndpoints
  {
    public static void Map(WebApplication app)
    {
      // Minimal API endpoint definitions
      app.MapGet("/", () => "Hello World!");


      // Route parameter example: /greeting/John
      app.MapGet("/greeting/{name}", (string name) =>
      {
        Log.Information("Greeting {Name}", name);
        return $"Hello, {name}!";
      });


      // Query parameter example: /hello?name=John
      app.MapGet("/hello", (string? name) =>
      {
        return $"Hello, {name ?? "Guest"}!";
      });

      app.MapGet("/test", async context =>
      {
        string? requestId = context.Request.Headers["x-cff-request-id"];

        await context.Response.WriteAsync($"Request ID is: {requestId}");
      });
    }
  }
}