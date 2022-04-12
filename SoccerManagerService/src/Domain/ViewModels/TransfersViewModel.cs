using Soccer.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Domain.Entities
{
    public class TransfersViewModel
    {
        public int Id { get; set; }
        public int PlayersId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string CurrentTeam { get; set; }
        public double AskingPrice { get; set; }
        public double MarketValue { get; set; }
        public TransferStatusEnum Status { get; set; }
    }
}
