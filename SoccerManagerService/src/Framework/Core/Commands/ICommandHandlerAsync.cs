// <copyright file="ICommandHandlerAsync.cs" company="Soccer">
// Copyright © 2015-2020 Soccer. All Rights Reserved.
// </copyright>

namespace Soccer.Platform.Infrastructure.Core.Commands
{
    using System.Threading.Tasks;
    using Soccer.Platform.Infrastructure.Core.Commands;

    /// <summary>Async command handler generic interface.</summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandlerAsync<in TCommand>
        where TCommand : Command
    {
        Task<CommandResponse> HandleAsync(TCommand command);
    }
}
