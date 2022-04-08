using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface ITeamService
    {
        public Teams GetNewTeam(Users user);
        public CommandResponse UpdateTeam(Teams team);
    }
}