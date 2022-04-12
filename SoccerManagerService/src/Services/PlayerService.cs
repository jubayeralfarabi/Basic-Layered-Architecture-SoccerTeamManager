using Services.Abstractions;
using Soccer.Domain.Entities;
using Soccer.Infrastructure.Repository.RDBRepository;
using Soccer.Models.Constants;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IReadWriteRepository readWriteRepository;

        public PlayerService(IReadWriteRepository readWriteRepository) 
        { 
            this.readWriteRepository = readWriteRepository;
        }

        public Players[] GeneratePlayersForTeam()
        {
            var players = new Players[] { };

            players = players.Concat(Enumerable.Range(0, 3).Select(s => this.GetNewPlayer(PlayerPositionsEnum.GoalKeeper))).ToArray();
            players = players.Concat(Enumerable.Range(0, 6).Select(s => this.GetNewPlayer(PlayerPositionsEnum.Midfielder))).ToArray();
            players = players.Concat(Enumerable.Range(0, 6).Select(s => this.GetNewPlayer(PlayerPositionsEnum.Defender))).ToArray();
            players = players.Concat(Enumerable.Range(0, 5).Select(s => this.GetNewPlayer(PlayerPositionsEnum.Attacker))).ToArray();

            return players;
        }

        public async Task<CommandResponse> UpdatePlayer(int playerId, string firstName, string lastName, string country, int userId)
        {
            var team = (await this.readWriteRepository.GetAsync<Teams>(t => t.UsersId == userId)).FirstOrDefault();
            var player = this.readWriteRepository.GetById<Players>(playerId);

            var response = new CommandResponse();

            if (player == null || player.TeamsId != team.Id)
            {
                response.ValidationResult.AddError("Invalid player id or you don't have permission to update this player");
                return response;
            }

            if (!string.IsNullOrEmpty(firstName)) player.FirstName = firstName;
            if (!string.IsNullOrEmpty(lastName)) player.LastName = lastName;
            if (!string.IsNullOrEmpty(country)) player.Country = country;

            this.readWriteRepository.Update<Players>(player);

            return response;
        }

        private Players GetNewPlayer(PlayerPositionsEnum position)
        {
            return new Players()
            {
                FirstName = "default" + new Random().Next(0,1000),
                LastName = "default",
                MarketValue = 1000000,
                Country = "default" + new Random().Next(0, 1000),
                Position = position,
                Age = new Random().Next(18,40),
            };
        }
    }
}