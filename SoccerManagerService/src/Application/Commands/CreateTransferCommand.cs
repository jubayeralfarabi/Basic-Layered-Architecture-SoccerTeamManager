namespace Soccer.Application.Commands
{
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class CreateTransferCommand : Command
    {
        public double AskingPrice { get; set; }
        public int PlayerId { get; set; }
    }
}