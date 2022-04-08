namespace Soccer.Application.Commands
{
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class CreateUserCommand : Command
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}