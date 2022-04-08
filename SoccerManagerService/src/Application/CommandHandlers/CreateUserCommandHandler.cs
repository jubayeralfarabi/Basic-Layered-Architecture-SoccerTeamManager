namespace Soccer.Application.CommandHandlers
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Services.Abstractions;
    using Soccer.Application.Commands;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class CreateUserCommandHandler : ICommandHandlerAsync<CreateUserCommand>
    {
        private ILogger<CreateUserCommandHandler> logger;
        private IUserService userService;

        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        public async Task<CommandResponse> HandleAsync(CreateUserCommand command)
        {
            return await this.userService.CreateUser(command.Email, command.FirstName, command.LastName, command.Password);
        }
    }
}
