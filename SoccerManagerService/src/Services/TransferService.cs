using Microsoft.EntityFrameworkCore;
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

            CommandResponse response = this.ValidateConfirmTransfer(transfer, fromTeam, toTeam);
            if (!response.ValidationResult.IsValid) return response;

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

            return response;
        }

        public async Task<CommandResponse> CreateTransfer(double askingPrice, int playerId, int userId)
        {
            var response = new CommandResponse();

            var team = (await this.readWriteRepository.GetAsync<Teams>(t => t.UsersId == userId)).FirstOrDefault();
            var player = this.readWriteRepository.GetById<Players>(playerId);

            if (player == null || player.TeamsId != team.Id) response.ValidationResult.AddError("Invalid player id or you don't have permission to update this player");
            if (askingPrice <= 0) response.ValidationResult.AddError("Invalid asking price");
            if(!response.ValidationResult.IsValid) return response;

            this.readWriteRepository.Create<Transfers>(new Transfers()
            {
                AskingPrice = askingPrice,
                FromTeamId = team.Id,
                Status = Soccer.Models.Constants.TransferStatusEnum.Active,
                PlayersId = playerId,
            });
            return response;
        }

        public CommandResponse GetAllActiveTransfers()
        {
            var result =  this.readWriteRepository.SqlRawQuery<TransfersViewModel>("select " +
                        "    t.Id," +
                        "    t.AskingPrice," +
                        "    p.MarketValue," +
                        "    t.Status," +
                        "    t.PlayersId," +
                        "    p.FirstName," +
                        "    p.LastName," +
                        "    p.Position," +
                        "    p.Age," +
                        "    p.Country," +
                        "    ft.Name CurrentTeam" +
                        " from Transfers t" +
                        " inner join Players p on p.Id = t.PlayersId" +
                        " inner join Teams ft on ft.Id = t.FromTeamId" +
                        " where t.Status = 0");
            return new CommandResponse() 
            {
                Result = result.ToList(),
            };
        }

        private CommandResponse ValidateConfirmTransfer(Transfers transfer, Teams fromTeam, Teams toTeam)
        {
            CommandResponse response = new CommandResponse();

            if (fromTeam.UsersId == toTeam.UsersId) response.ValidationResult.AddError("Transfer should be confirmed between different teams");
            if (toTeam.AvailableCash < transfer.AskingPrice) response.ValidationResult.AddError("Teams doesn't have sufficient cash");

            return response;
        }
    }
}