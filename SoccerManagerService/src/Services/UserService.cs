using Framework.Core;
using Services.Abstractions;
using Soccer.Domain.Entities;
using Soccer.Infrastructure.Repository.RDBRepository;
using Soccer.Platform.Infrastructure.Core.Commands;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IReadWriteRepository readWriteRepository;
        private readonly ITeamService teamService;
        private readonly ISecurityContext securityContext;

        public UserService(IReadWriteRepository readWriteRepository, ITeamService teamService, ISecurityContext securityContext)
        {
            this.readWriteRepository = readWriteRepository;
            this.teamService = teamService;
            this.securityContext = securityContext;
        }

        public async Task<CommandResponse> CreateUser(string Email, string FirstName, string LastName, string Password)
        {
            Users user = GetUser(Email, FirstName, LastName, Password);

            var result = await this.ValidateCreateUser(user);

            if (!result.ValidationResult.IsValid) return result;

            user.Team = this.teamService.GetNewTeam(user);

            this.readWriteRepository.Create<Users>(user);

            return new CommandResponse();
        }

        public async Task<CommandResponse> LoginUser(string Email, string Password)
        {
            Users user = (await this.readWriteRepository.GetAsync<Users>(u => u.Email == Email)).FirstOrDefault();

            var response = new CommandResponse();

            if (user == null)
            {
                response.ValidationResult.AddError("Invalid email");
                return response;
            }
            if (user.Password != this.ComputeSHA256Hash(Password)) response.ValidationResult.AddError("Invalid password");

            if (!response.ValidationResult.IsValid) return response;

            user.Team = (await this.teamService.GetMyTeam(user.Id)).Result as Teams;

            response.Result = new 
            {
                AccessToken = this.securityContext.GenerateJSONWebToken(user.Id, user.Email, user.FirstName, user.LastName),
                User = user,
            };

            return response;
        }

        private async Task<CommandResponse> ValidateCreateUser(Users user)
        {
            var result = new CommandResponse();

            var users = await this.readWriteRepository.GetAsync<Users>(u => u.Email == user.Email);
            if(users.Any()) result.ValidationResult.AddError("Invalid email. User already exists");

            if (!new EmailAddressAttribute().IsValid(user.Email)) result.ValidationResult.AddError("Invalid email");
            if (string.IsNullOrEmpty(user.Password)) result.ValidationResult.AddError("Invalid password");
            if (string.IsNullOrEmpty(user.FirstName)) result.ValidationResult.AddError("Invalid firstname");
            if (string.IsNullOrEmpty(user.LastName)) result.ValidationResult.AddError("Invalid lastname");
            return result;
        }

        private Users GetUser(string Email, string FirstName, string LastName, string Password)
        {
            return new Users()
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = this.ComputeSHA256Hash(Password),
            };
        }

        private string ComputeSHA256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}