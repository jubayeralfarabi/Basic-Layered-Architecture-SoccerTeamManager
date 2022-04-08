namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class CreateTransferCommandHandler : ICommandHandlerAsync<CreateTransferCommand>
    {
        private ILogger<CreateTransferCommandHandler> logger;
        private ITransferService transferService;

        public CreateTransferCommandHandler(
            ILogger<CreateTransferCommandHandler> logger, ITransferService transferService)
        {
            this.logger = logger;
            this.transferService = transferService;
        }

        public async Task<CommandResponse> HandleAsync(CreateTransferCommand command)
        {
            return this.transferService.CreateTransfer(command.AskingPrice, command.PlayerId, command.FromTeamId);
        }
    }
}
