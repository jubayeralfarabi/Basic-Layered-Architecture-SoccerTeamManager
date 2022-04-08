// <copyright file="ICommandSender.cs" company="Soccer">
// Copyright © 2015-2020 Soccer. All Rights Reserved.
// </copyright>

namespace Soccer.Platform.Infrastructure.Core.Commands
{
    using System.Threading.Tasks;

    /// <summary>Command sender interface.</summary>
    public interface ICommandSender
    {
        Task<CommandResponse> SendAsync(Command command);
    }
}
