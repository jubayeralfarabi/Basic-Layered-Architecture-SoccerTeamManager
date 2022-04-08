namespace Soccer.Platform.Infrastructure.Core
{
    using System.Threading.Tasks;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class Dispatcher : IDispatcher
    {
        private readonly ICommandSender commandSender;

        public Dispatcher(
            ICommandSender commandSender)
        {
            this.commandSender = commandSender;
        }

        public Task<CommandResponse> SendAsync(Command command)
        {
            return this.commandSender.SendAsync(command);
        }
    }
}