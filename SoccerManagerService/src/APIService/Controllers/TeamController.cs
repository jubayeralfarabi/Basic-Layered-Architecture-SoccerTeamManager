namespace Soccer.APIService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core;
    using Soccer.Platform.Infrastructure.Core.Commands;
    using Services.Abstractions;
    using Framework.Core;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService teamService;
        private readonly ISecurityContext securityContext;
        private readonly IDispatcher dispatcher;

        public TeamController(ITeamService teamService, IDispatcher dispatcher, ISecurityContext securityContext)
        {
            this.teamService = teamService;
            this.dispatcher = dispatcher;
            this.securityContext = securityContext;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamCommand command)
        {
            return CheckResponse(await this.dispatcher.SendAsync(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTeam()
        {
            return CheckResponse(await this.teamService.GetMyTeam(this.securityContext.UserContext.UserId));
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