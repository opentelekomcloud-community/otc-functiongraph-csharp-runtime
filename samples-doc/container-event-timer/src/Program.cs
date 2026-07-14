namespace container_event_timer;

using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelekomCloud.Serverless.Function.Common;
using OpenTelekomCloud.Serverless.Function.Events.Timer;
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
    // builder.WebHost.UseUrls("http://*:8000");

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

    var app = builder.Build();

    // Use the custom logging middleware
    app.UseMiddleware<LoggingMiddleware>();


    // Map API endpoints
    APIEndpoints.Map(app);


    // Start the application
    app.Run();
  }

  public static class APIEndpoints
  {
    public static void Map(WebApplication app)
    {

      app.MapPost("/invoke", async ([FromHeader(Name = "x-cff-request-id")] string? requestId, HttpContext context) =>
      {
        try
        {
          // Read the request body into a MemoryStream (seekable)
          var ms = new MemoryStream();
          await context.Request.Body.CopyToAsync(ms);
          ms.Position = 0;

          var serializer = new JsonSerializer();
          var timerEvent = serializer.Deserialize<TimerEvent>(ms);

          Log.Information("Timer Event {Event}", timerEvent?.ToString() ?? "null");
          // Log.Information("Project ID: {ProjectId}", context.Request.Headers["x-cff-project-id"].ToString());
          var BUILD_TIMESTAMP = Environment.GetEnvironmentVariable("BUILD_TIMESTAMP") ?? "unknown";
          Log.Information("Build Timestamp: {BuildTimestamp}", BUILD_TIMESTAMP);
          
          // Single response write
          var response = $"Processed event: {timerEvent?.ToString() ?? "unknown"}, RequestId: {requestId}";
          await context.Response.WriteAsync(response);
        }
        catch (Exception ex)
        {
          Log.Error(ex, "Error deserializing event");
          context.Response.StatusCode = 400;
          await context.Response.WriteAsync($"Error: {ex.Message}");
        }
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
