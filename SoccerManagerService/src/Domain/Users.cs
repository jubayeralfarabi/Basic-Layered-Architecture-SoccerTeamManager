using System.ComponentModel.DataAnnotations;

namespace Soccer.Domain.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Teams Team { get; set; }
    }
}
