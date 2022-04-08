using Services.Abstractions;
using Soccer.Domain.Entities;
using Soccer.Models.Constants;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services
{
    public class PlayerService : IPlayerService
    {
        public Players[] GeneratePlayersForTeam()
        {
            var players = new Players[] { };

            players = players.Concat(Enumerable.Range(0, 3).Select(s => this.GetNewPlayer(PlayerPositionsEnum.GoalKeeper))).ToArray();
            players = players.Concat(Enumerable.Range(0, 6).Select(s => this.GetNewPlayer(PlayerPositionsEnum.Midfielder))).ToArray();
            players = players.Concat(Enumerable.Range(0, 6).Select(s => this.GetNewPlayer(PlayerPositionsEnum.Defender))).ToArray();
            players = players.Concat(Enumerable.Range(0, 5).Select(s => this.GetNewPlayer(PlayerPositionsEnum.Attacker))).ToArray();

            return players;
        }

        public CommandResponse UpdateTeam(Teams team)
        {
            throw new NotImplementedException();
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