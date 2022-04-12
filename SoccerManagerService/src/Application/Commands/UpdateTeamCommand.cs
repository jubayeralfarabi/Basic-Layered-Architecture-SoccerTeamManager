namespace Soccer.Application.Commands
{
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class UpdateTeamCommand : Command
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
    }
}