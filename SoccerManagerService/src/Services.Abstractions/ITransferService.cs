using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface ITransferService
    {
        public CommandResponse CreateTransfer(double askingPrice, int playerId, int fromTeamId);
        public Task<CommandResponse> ConfirmTransfer(int transferId, int userId);
    }
}