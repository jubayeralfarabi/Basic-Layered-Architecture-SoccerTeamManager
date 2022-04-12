namespace Soccer.Application.Commands
{
    using Soccer.Platform.Infrastructure.Core.Commands;

    public class ConfirmTransferCommand : Command
    {
        public int TransferId { get; set; }
    }
}