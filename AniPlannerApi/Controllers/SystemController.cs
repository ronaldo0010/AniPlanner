using Microsoft.AspNetCore.Mvc;

namespace AniPlannerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetSystemStatus()
    {
        return Ok("version: 1.0.0");
    }
}