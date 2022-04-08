using Soccer.Domain.Entities;
using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface IPlayerService
    {
        public Players[] GeneratePlayersForTeam();
    }
}