using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using Serilog.Events;
using Serilog.Expressions;

/// <summary>
/// Middleware to log the x-cff-request-id header for each incoming HTTP request.
/// </summary>
public class LoggingMiddleware
{
  readonly RequestDelegate _next;

  /// <summary>
  /// Constructor for LoggingMiddleware
  /// </summary>
  /// <param name="next"></param>
  public LoggingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  /// <summary>
  /// Invoke method to process the HTTP request
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public async Task Invoke(HttpContext context)
  {
    context.Request.Headers.TryGetValue("X-CFF-Request-Id", out StringValues requestId);

    context.Request.Headers.TryGetValue("X-CFF-Memory", out StringValues memory);
    context.Request.Headers.TryGetValue("X-CFF-Timeout", out StringValues timeout);
    context.Request.Headers.TryGetValue("X-CFF-Func-Version", out StringValues funcVersion);
    context.Request.Headers.TryGetValue("X-CFF-Func-Name", out StringValues funcName);
    context.Request.Headers.TryGetValue("X-CFF-Project-Id", out StringValues projectId);
    context.Request.Headers.TryGetValue("X-CFF-Package", out StringValues package);
    context.Request.Headers.TryGetValue("X-CFF-Region", out StringValues region);

    using (LogContext.PushProperty("XCFFRequestId", requestId.FirstOrDefault(), true))
    using (LogContext.PushProperty("XCFFMemory", memory.FirstOrDefault()))
    using (LogContext.PushProperty("XCFFTimeout", timeout.FirstOrDefault()))
    using (LogContext.PushProperty("XCFFFuncVersion", funcVersion.FirstOrDefault()))
    using (LogContext.PushProperty("XCFFFuncName", funcName.FirstOrDefault()))
    using (LogContext.PushProperty("XCFFProjectId", projectId.FirstOrDefault()))
    using (LogContext.PushProperty("XCFFPackage", package.FirstOrDefault()))
    using (LogContext.PushProperty("XCFFRegion", region.FirstOrDefault()))
    {
      await _next.Invoke(context);
    }
  }

}

/// <summary>
/// Static class containing DateTime conversion functions for Serilog.Expressions
/// </summary>
static class DateTimeFunctions
{
  /// <summary>
  /// Converts a DateTime or DateTimeOffset LogEventPropertyValue to UTC.
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static LogEventPropertyValue? ToUtc(LogEventPropertyValue? value)
  {
    if (value is ScalarValue scalar)
    {
      if (scalar.Value is DateTimeOffset dto)
        return new ScalarValue(dto.UtcDateTime);

      if (scalar.Value is DateTime dt)
        return new ScalarValue(dt.ToUniversalTime());
    }

    return null;
  }
}