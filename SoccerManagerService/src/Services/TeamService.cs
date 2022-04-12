using Services.Abstractions;
using Soccer.Domain.Entities;
using Soccer.Infrastructure.Repository.RDBRepository;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services
{
    public class TeamService : ITeamService
    {
        private readonly IPlayerService playerService;
        private readonly IReadWriteRepository readWriteRepository;

        public TeamService(IPlayerService playerService, IReadWriteRepository readWriteRepository)
        {
            this.playerService = playerService;
            this.readWriteRepository = readWriteRepository;
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

        public async Task<CommandResponse> UpdateTeam(string name, string country, int userId)
        {
            var team = (await this.readWriteRepository.GetAsync<Teams>(t => t.UsersId == userId)).FirstOrDefault();

            if(!string.IsNullOrEmpty(name)) team.Name = name;
            if(!string.IsNullOrEmpty(country)) team.Country = country;

            this.readWriteRepository.Update<Teams>(team);

            return new CommandResponse();
        }

        public async Task<CommandResponse> GetMyTeam(int userId)
        {
            var team = (await this.readWriteRepository.GetAsync<Teams>(t => t.UsersId == userId)).FirstOrDefault();
            team.Players = (await this.readWriteRepository.GetAsync<Players>(t => t.TeamsId == team.Id)).ToList();

            return new CommandResponse()
            {
                Result = team,
            };
        }
    }
}