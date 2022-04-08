using Soccer.Domain;
using Soccer.Domain.Entities;
using Soccer.Domain.Events;
using Soccer.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace Domain.UnitTests
{
    public class AlterationAggregateTest
    {
        public AlterationDetails[] ValidAlterationDetails => new AlterationDetails[] { new AlterationDetails() {  AlterationName = AlterationTypeEnum.SleeveRight, AlterationValue = 1,Id=0} };
        public AlterationDetails[] InvalidAlterationDetails_Value => new AlterationDetails[] { new AlterationDetails() {  AlterationName = AlterationTypeEnum.SleeveRight, AlterationValue = 6,Id=0} };
        public AlterationDetails[] InvalidAlterationDetails_Value1 => new AlterationDetails[] { new AlterationDetails() { AlterationName = AlterationTypeEnum.SleeveRight, AlterationValue = -6, Id = 0 } };
        public AlterationDetails[] InvalidAlterationDetails => new AlterationDetails[] { };
        public const string customerId = "customer";

        [Fact]
        public void CreateAlteration_Success()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);

            Assert.True(alteration.Events.Last() is AlterationCreatedEvent);
        }

        [Fact]
        public void CreateAlteration_Failed_InvalidAlterationDetail_Value()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, InvalidAlterationDetails_Value, customerId);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);

            alteration.CreateAlteration(id, InvalidAlterationDetails_Value1, customerId);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }

        [Fact]
        public void CreateAlteration_Failed_InvalidCustomerId()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, String.Empty);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }

        [Fact]
        public void CreateAlteration_Failed_InvalidAlterationId()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.Empty;

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }

        [Fact]
        public void CreateAlteration_Failed_InvalidAlterationDegtails()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, InvalidAlterationDetails, customerId);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }

        [Fact]
        public void CreateAlteration_Failed_InvalidAlterationDegtailsNull()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, null, customerId);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }


        [Fact]
        public void CompletePayment_Success()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);
            alteration.CompletePayment(id);

            Assert.True(alteration.Events.Last() is AlterationPaymentDoneEvent);
        }

        [Fact]
        public void CompletePayment_Failed_AlreadyPaid()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);
            alteration.CompletePayment(id);
            alteration.CompletePayment(id);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }


        [Fact]
        public void StartProcessing_Success()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);
            alteration.CompletePayment(id);
            alteration.StartProcessing(id);

            Assert.True(alteration.Events.Last() is AlterationStartedProcessingEvent);
        }

        [Fact]
        public void StartProcessing_Failed_PaymentRequired()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);
            alteration.StartProcessing(id);

            Assert.True(alteration.Events.Last() is AlterationBusinessRuleViolationEvent);
        }

        [Fact]
        public void FinishAlteration_Success()
        {
            AlterationAggregate alteration = new AlterationAggregate();

            Guid id = Guid.NewGuid();

            alteration.CreateAlteration(id, ValidAlterationDetails, customerId);
            alteration.CompletePayment(id);
            alteration.StartProcessing(id);
            alteration.FinishAlteration(id);

            Assert.True(alteration.Events.Last() is AlterationFinishedEvent);
        }
    }
}