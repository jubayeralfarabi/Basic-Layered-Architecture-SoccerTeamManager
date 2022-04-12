namespace Soccer.Application.Commands
{
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class UpdatePlayerCommand : Command
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
    }
}