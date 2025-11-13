namespace http_minimalWebAPI;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(); 

    var app = builder.Build();

    // Enable serving static files (including favicon.ico from wwwroot)
    app.UseStaticFiles();

    // Minimal API endpoint definitions
    app.MapGet("/", () => "Hello World!");

    // Route parameter example: /greeting/John
    app.MapGet("/greeting/{name}", (string name) =>
    {
      return $"Hello, {name}!";
    });


    // Query parameter example: /hello?name=John
    app.MapGet("/hello", (string? name) =>
    {
      return $"Hello, {name ?? "Guest"}!";
    });

    app.MapGet("/test", async context =>
    {
      string requestId = context.Request.Headers["x-cff-request-id"];

      await context.Response.WriteAsync($"Request ID is: {requestId}");
    });

    // Start the application
    app.Run();
  }
}