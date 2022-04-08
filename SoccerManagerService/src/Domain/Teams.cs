using System.ComponentModel.DataAnnotations;

namespace Soccer.Domain.Entities
{
    public class Teams
    {
        [Key]
        public int Id { get; set; }
        public double AvailableCash { get; set; }
        public double TeamValue { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public int UsersId { get; set; }
        public ICollection<Players> Players { get; set; }
    }
}
