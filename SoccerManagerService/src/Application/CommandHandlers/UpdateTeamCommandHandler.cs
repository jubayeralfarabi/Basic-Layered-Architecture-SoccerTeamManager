namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Framework.Core;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class UpdateTeamCommandHandler : ICommandHandlerAsync<UpdateTeamCommand>
    {
        private ILogger<UpdateTeamCommandHandler> logger;
        private ITeamService teamService;
        private ISecurityContext securityContext;

        public UpdateTeamCommandHandler(
            ILogger<UpdateTeamCommandHandler> logger, ITeamService teamService, ISecurityContext securityContext)
        {
            this.logger = logger;
            this.teamService = teamService;
            this.securityContext = securityContext;
        }

        public async Task<CommandResponse> HandleAsync(UpdateTeamCommand command)
        {
            return await this.teamService.UpdateTeam(command.Name, command.Country, this.securityContext.UserContext.UserId);
        }
    }
}
