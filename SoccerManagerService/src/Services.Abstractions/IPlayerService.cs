using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface IPlayerService
    {
        public Players[] GeneratePlayersForTeam();
        public Task<CommandResponse> UpdatePlayer(int playerId, string firstName, string lastName, string country, int userId);
    }
}