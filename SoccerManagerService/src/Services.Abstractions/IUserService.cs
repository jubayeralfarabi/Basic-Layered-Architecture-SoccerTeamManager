using Soccer.Platform.Infrastructure.Core.Commands;

namespace Services.Abstractions
{
    public interface IUserService
    {
        public Task<CommandResponse> CreateUser(string Email, string FirstName, string LastName, string Password);
    }
}