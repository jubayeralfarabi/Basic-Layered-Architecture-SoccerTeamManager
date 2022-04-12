namespace Soccer.APIService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core;
    using Soccer.Platform.Infrastructure.Core.Commands;
    using Services.Abstractions;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService transferService;
        private readonly IDispatcher dispatcher;

        public TransferController(ILogger<TransferController> logger, IDispatcher dispatcher, ITransferService transferService)
        {
            this.transferService = transferService;
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

        [HttpGet]
        public IActionResult GetActiveTransfers()
        {
            return CheckResponse(this.transferService.GetAllActiveTransfers());
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