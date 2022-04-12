namespace Framework.Core
{
    public interface ISecurityContext
    {
        UserContext UserContext { get; set; }
        string GenerateJSONWebToken(int userId, string email, string firstName, string lastName);
    }
}
