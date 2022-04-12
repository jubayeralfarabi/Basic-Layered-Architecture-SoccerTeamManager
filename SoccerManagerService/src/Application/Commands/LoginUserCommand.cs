namespace Soccer.Application.Commands
{
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class LoginUserCommand : Command
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}