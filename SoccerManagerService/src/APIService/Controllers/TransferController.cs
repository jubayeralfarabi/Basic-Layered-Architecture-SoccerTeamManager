namespace Soccer.APIService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core;
    using Soccer.Platform.Infrastructure.Core.Commands;

    [ApiController]
    [Route("[controller]/[action]")]
    public class TransferController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IDispatcher dispatcher;

        public TransferController(ILogger<UserController> logger, IDispatcher dispatcher)
        {
            this.logger = logger;
            this.dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferCommand command)
        {
            return CheckResponse(await this.dispatcher.SendAsync(command));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmTransfer([FromBody] ConfirmTransferCommand command)
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