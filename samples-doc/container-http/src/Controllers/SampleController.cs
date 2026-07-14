using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace http_minimalWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
  private readonly ILogger<SampleController> _logger;
  public SampleController(ILogger<SampleController> logger)
  {
    _logger = logger;
  }

  /// <summary>
  /// GET method for SampleController
  /// </summary>
  /// <returns></returns>
  [HttpGet(Name = "GetSample")]
  public IActionResult Get()
  {
    _logger.LogInformation("SampleController Get method called.");
    return Ok(new { Message = "Hello from SampleController!" });
  }
}

