namespace http_minimalWebAPI;

using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Expressions;
using Serilog.Extensions.Hosting;
using Serilog.Templates;

public class Program
{

  public static void Main(string[] args)
  {
    var BUILD_TIMESTAMP = Environment.GetEnvironmentVariable("BUILD_TIMESTAMP") ?? "unknown";

    Console.WriteLine($"####################### starting version: {BUILD_TIMESTAMP}");
    // Create the WebApplication builder
    var contentRoot = Environment.GetEnvironmentVariable("ASPNETCORE_CONTENTROOT") ?? AppContext.BaseDirectory;
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
      Args = args,
      ContentRootPath = contentRoot,
      WebRootPath = Path.Combine(contentRoot, "wwwroot")
    });

    // timezone conversion functions for Serilog.Expressions
    var dateTimeFunctions = new StaticMemberNameResolver(typeof(DateTimeFunctions));

    // Configure Serilog to log requestId and timestamp in UTC
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

    // Add controller services
    builder.Services.AddControllers();

    // Add Swagger services
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo { Title = "FunctionGraph HTTP Function in Container", Version = "v1" });

      var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath= Path.Combine(baseDirectory, xmlFilename);
      // Console.WriteLine($"########################################### Including XML comments from: {xmlPath}");

      options.IncludeXmlComments(xmlPath);

    });

    var app = builder.Build();

    // Use the custom logging middleware
    app.UseMiddleware<LoggingMiddleware>();

    // Enable serving static files (including favicon.ico from wwwroot)
    app.UseStaticFiles();

    // Enable Swagger middleware
    string useSwaggerUI = (Environment.GetEnvironmentVariable("USE_SWAGGER_UI") ?? "false").ToLower();
    if (useSwaggerUI.Equals("true"))
    {
      
      app.UseSwagger(options =>
      {
        options.RouteTemplate = "swagger/{documentName}/swagger.json";
      });

      // Some gateways do not reliably handle /swagger -> /swagger/index.html redirects.
      app.UseRewriter(new RewriteOptions().AddRewrite("^swagger$", "swagger/index.html", true));

      app.UseSwaggerUI(options =>
      {
        options.RoutePrefix = "swagger";
        options.SwaggerEndpoint("v1/swagger.json", "FunctionGraph HTTP Function in Container v1");
      });
    }

    // Map API endpoints (e.g in case of not using controllers)
    APIEndpoints.Map(app);

    // Map controller routes
    app.MapControllers();

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

      app.MapGet("/version", () =>
      {
        var BUILD_TIMESTAMP = Environment.GetEnvironmentVariable("BUILD_TIMESTAMP") ?? "unknown";
        var ASPNETCORE_CONTENTROOT = Environment.GetEnvironmentVariable("ASPNETCORE_CONTENTROOT") ?? "unknown";

        Log.Information("Build Timestamp: {BuildTimestamp}", BUILD_TIMESTAMP);
        Log.Information("Content Root: {ContentRoot}", ASPNETCORE_CONTENTROOT);
        return $"BUILD_TIMESTAMP:  {BUILD_TIMESTAMP}, ASPNETCORE_CONTENTROOT: {ASPNETCORE_CONTENTROOT}";
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

      app.MapPost("/init", async context =>
      {
        string? requestId = context.Request.Headers["x-cff-request-id"];

        Log.Information($"Init called with Request ID: {requestId}");
        await context.Response.WriteAsync($"Request ID is: {requestId}");
      });

    }
  }

}
