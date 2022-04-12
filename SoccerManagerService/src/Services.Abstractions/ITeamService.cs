using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface ITeamService
    {
        public Teams GetNewTeam(Users user);
        public Task<CommandResponse> UpdateTeam(string name, string country, int userId);
        public Task<CommandResponse> GetMyTeam(int userId);
    }
}