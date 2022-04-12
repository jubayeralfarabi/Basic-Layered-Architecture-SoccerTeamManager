namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class LoginUserCommandHandler : ICommandHandlerAsync<LoginUserCommand>
    {
        private ILogger<LoginUserCommandHandler> logger;
        private IUserService userService;

        public LoginUserCommandHandler(
            ILogger<LoginUserCommandHandler> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        public async Task<CommandResponse> HandleAsync(LoginUserCommand command)
        {
            return await this.userService.LoginUser(command.Email, command.Password);
        }
    }
}
