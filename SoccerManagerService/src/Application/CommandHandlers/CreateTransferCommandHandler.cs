namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Framework.Core;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class CreateTransferCommandHandler : ICommandHandlerAsync<CreateTransferCommand>
    {
        private ILogger<CreateTransferCommandHandler> logger;
        private ITransferService transferService;
        private ISecurityContext securityContext;

        public CreateTransferCommandHandler(
            ILogger<CreateTransferCommandHandler> logger, ITransferService transferService, ISecurityContext securityContext)
        {
            this.logger = logger;
            this.transferService = transferService;
            this.securityContext = securityContext;
        }

        public async Task<CommandResponse> HandleAsync(CreateTransferCommand command)
        {
            return await this.transferService.CreateTransfer(command.AskingPrice, command.PlayerId, securityContext.UserContext.UserId);
        }
    }
}
