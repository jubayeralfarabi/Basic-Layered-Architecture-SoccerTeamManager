using Services.Abstractions;
using Soccer.Domain.Entities;
using Soccer.Infrastructure.Repository.RDBRepository;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services
{
    public class TransferService : ITransferService
    {
        private readonly IReadWriteRepository readWriteRepository;

        public TransferService(IReadWriteRepository readWriteRepository)
        {
            this.readWriteRepository = readWriteRepository;
        }

        public async Task<CommandResponse> ConfirmTransfer(int transferId, int userId)
        {
            var transfer = this.readWriteRepository.GetById<Transfers>(transferId);
            var player = this.readWriteRepository.GetById<Players>(transfer.PlayersId);
            var fromTeam = this.readWriteRepository.GetById<Teams>(player.TeamsId);
            var toTeam = (await this.readWriteRepository.GetAsync<Teams>(t => t.UsersId == userId)).FirstOrDefault();

            fromTeam.TeamValue = fromTeam.TeamValue - player.MarketValue;
            fromTeam.AvailableCash = fromTeam.AvailableCash + transfer.AskingPrice;
            this.readWriteRepository.Update(fromTeam);

            player.TeamsId = toTeam.Id;
            player.MarketValue = transfer.AskingPrice + transfer.AskingPrice * (new Random().Next(10, 100) / 100);
            this.readWriteRepository.Update(player);

            toTeam.TeamValue = toTeam.TeamValue + player.MarketValue;
            toTeam.AvailableCash = toTeam.AvailableCash - transfer.AskingPrice;
            this.readWriteRepository.Update(toTeam);

            transfer.Status = Soccer.Models.Constants.TransferStatusEnum.Transferred;
            transfer.ToTeamId = toTeam.Id;
            this.readWriteRepository.Update(transfer);

            return new CommandResponse();
        }

        public CommandResponse CreateTransfer(double askingPrice, int playerId, int fromTeamId)
        {
            this.readWriteRepository.Create<Transfers>(new Transfers()
            {
                AskingPrice = askingPrice,
                FromTeamId = fromTeamId,
                Status = Soccer.Models.Constants.TransferStatusEnum.Active,
                PlayersId = playerId,
            });
            return new CommandResponse();
        }
    }
}