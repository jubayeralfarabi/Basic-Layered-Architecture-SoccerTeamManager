using Soccer.Models.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer.Domain.Entities
{
    public class Players
    {
        [Key]
        public int Id { get; set; }
        public PlayerPositionsEnum Position { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public double MarketValue { get; set; }

        public int TeamsId { get; set; }
    }
}
