namespace Soccer.APIService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core;
    using Soccer.Platform.Infrastructure.Core.Commands;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> logger;
        private readonly IDispatcher dispatcher;

        public PlayerController(ILogger<PlayerController> logger, IDispatcher dispatcher)
        {
            this.logger = logger;
            this.dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerCommand command)
        {
            return CheckResponse(await this.dispatcher.SendAsync(command));
        }

        private IActionResult CheckResponse(CommandResponse response)
        {
            if (!response.ValidationResult.IsValid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}