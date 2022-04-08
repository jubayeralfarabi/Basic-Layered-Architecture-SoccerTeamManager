namespace Soccer.Platform.Infrastructure.Core.Commands
{
    using Soccer.Platform.Infrastructure.Core.Validation;
    using System;
    using System.Threading.Tasks;

    public class CommandSender : ICommandSender
    {
        private readonly IServiceProvider serviceProvider;

        public CommandSender(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<CommandResponse> SendAsync(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());
            var handler = this.serviceProvider.GetService(handlerType);

            var handleMethod = handler.GetType().GetMethod("HandleAsync", new[] { command.GetType() });
            var response = await (Task<CommandResponse>)handleMethod.Invoke(handler, new object[] { command });

            if (response == null)
            {
                return null;
            }

            return new CommandResponse(response.ValidationResult != null ? response.ValidationResult : new ValidationResponse(), response.Result);
        }
    }
}
