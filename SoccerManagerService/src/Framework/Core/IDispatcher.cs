namespace Soccer.Platform.Infrastructure.Core
{
    using System.Threading.Tasks;
    using Soccer.Platform.Infrastructure.Core.Commands;

    public interface IDispatcher
    {
        Task<CommandResponse> SendAsync(Command command);
    }
}
