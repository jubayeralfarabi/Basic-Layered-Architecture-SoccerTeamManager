namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Framework.Core;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class ConfirmTransferCommandHandler : ICommandHandlerAsync<ConfirmTransferCommand>
    {
        private ILogger<ConfirmTransferCommandHandler> logger;
        private ITransferService transferService;
        private ISecurityContext securityContext;

        public ConfirmTransferCommandHandler(
            ILogger<ConfirmTransferCommandHandler> logger, ITransferService transferService, ISecurityContext securityContext)
        {
            this.logger = logger;
            this.transferService = transferService;
            this.securityContext = securityContext; 
        }

        public async Task<CommandResponse> HandleAsync(ConfirmTransferCommand command)
        {
            return await this.transferService.ConfirmTransfer(command.TransferId, this.securityContext.UserContext.UserId);
        }
    }
}
