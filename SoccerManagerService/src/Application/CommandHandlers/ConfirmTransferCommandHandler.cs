namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class ConfirmTransferCommandHandler : ICommandHandlerAsync<ConfirmTransferCommand>
    {
        private ILogger<ConfirmTransferCommandHandler> logger;
        private ITransferService transferService;

        public ConfirmTransferCommandHandler(
            ILogger<ConfirmTransferCommandHandler> logger, ITransferService transferService)
        {
            this.logger = logger;
            this.transferService = transferService;
        }

        public async Task<CommandResponse> HandleAsync(ConfirmTransferCommand command)
        {
            return await this.transferService.ConfirmTransfer(command.TransferId, command.UserId);
        }
    }
}
