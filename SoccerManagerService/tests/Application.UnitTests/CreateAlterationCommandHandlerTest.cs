using Microsoft.Extensions.Logging;
using Moq;
using Soccer.Application.CommandHandlers;
using Soccer.Application.Commands;
using Soccer.Application.Commands.Models;
using Soccer.Domain;
using Soccer.Domain.Entities;
using Soccer.Domain.ValueObjects;
using Soccer.Platform.Infrastructure.Core.Commands;
using Soccer.Platform.Infrastructure.Core.Domain;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests
{
    public class CreateAlterationCommandHandlerTest
    {
        public AlterationDetailsApplication[] ValidAlterationDetails => new AlterationDetailsApplication[] { new AlterationDetailsApplication() { AlterationName = AlterationTypeApplicationEnum.SleeveRight, AlterationValue = 1, Id = 0 } };
        public const string customerId = "customer";
        private readonly CreateAlterationCommandHandler commandHandler;
        private readonly ILogger<CreateAlterationCommandHandler> logger;
        private readonly IAggregateRepository<AlterationAggregate> aggregateRepository;

        public CreateAlterationCommandHandlerTest()
        {
            this.logger = new Mock<ILogger<CreateAlterationCommandHandler>>().Object;

            this.aggregateRepository = new Mock<IAggregateRepository<AlterationAggregate>>().Object;
            this.commandHandler = new CreateAlterationCommandHandler(this.logger, this.aggregateRepository);
        }

        [Fact]
        public async Task HandleAsync_Success()
        {
            CreateAlterationCommand command = new CreateAlterationCommand()
            {
                AlterationDetails = ValidAlterationDetails,
                AlterationId = Guid.NewGuid(),
                CustomerId = customerId,
            };

            CommandResponse response = await this.commandHandler.HandleAsync(command);

            Assert.True(response.ValidationResult.IsValid);
        }

        [Fact]
        public async Task HandleAsync_Failed()
        {
            CreateAlterationCommand command = new CreateAlterationCommand()
            {
            };

            CommandResponse response = await this.commandHandler.HandleAsync(command);

            Assert.False(response.ValidationResult.IsValid);
        }
    }
}