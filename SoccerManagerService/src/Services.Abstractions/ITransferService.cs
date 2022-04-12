using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface ITransferService
    {
        public Task<CommandResponse> CreateTransfer(double askingPrice, int playerId, int userId);
        public Task<CommandResponse> ConfirmTransfer(int transferId, int userId);
        public CommandResponse GetAllActiveTransfers();
    }
}