using System;
using Microsoft.AspNetCore.Mvc;

namespace PipelinePractice.Controllers
{
    [ApiController]
    [Route("api")]
    public class HealthController : ControllerBase
    {
        [HttpGet("health-check")]
        public IActionResult Get()
        {
            var result = new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow
            };

            return Ok(result);
        }
    }
}
