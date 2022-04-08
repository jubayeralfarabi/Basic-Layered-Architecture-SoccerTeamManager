using Soccer.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Domain.Entities
{
    public class Transfers
    {
        [Key]
        public int Id { get; set; }
        public Players Player { get; set; }
        public int PlayersId { get; set; }
        public int FromTeamId { get; set; }
        public int? ToTeamId { get; set; }
        public double AskingPrice { get; set; }
        public TransferStatusEnum Status { get; set; }
    }
}
