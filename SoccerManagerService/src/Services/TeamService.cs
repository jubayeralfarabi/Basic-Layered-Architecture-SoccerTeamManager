using Services.Abstractions;
using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services
{
    public class TeamService : ITeamService
    {
        private readonly IPlayerService playerService;
        public TeamService(IPlayerService playerService)
        {
            this.playerService = playerService;
        }
        public Teams GetNewTeam(Users user)
        {
            var team = new Teams()
            {
                AvailableCash = 5000000,
                Country = "default",
                Name = "Team " + user.FirstName,
                Players = this.playerService.GeneratePlayersForTeam(), 
            };
            team.TeamValue = team.Players.Sum(p => p.MarketValue);
            return team;
        }

        public CommandResponse UpdateTeam(Teams team)
        {
            throw new NotImplementedException();
        }
    }
}