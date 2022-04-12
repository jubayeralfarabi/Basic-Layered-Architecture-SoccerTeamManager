namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Framework.Core;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class UpdatePlayerCommandHandler : ICommandHandlerAsync<UpdatePlayerCommand>
    {
        private ILogger<UpdatePlayerCommandHandler> logger;
        private IPlayerService playerService;
        private ISecurityContext securityContext;

        public UpdatePlayerCommandHandler(
            ILogger<UpdatePlayerCommandHandler> logger, IPlayerService playerService, ISecurityContext securityContext)
        {
            this.logger = logger;
            this.playerService = playerService;
            this.securityContext = securityContext;
        }

        public async Task<CommandResponse> HandleAsync(UpdatePlayerCommand command)
        {
            return await this.playerService.UpdatePlayer(command.PlayerId, command.FirstName, command.LastName, command.Country, this.securityContext.UserContext.UserId);
        }
    }
}
