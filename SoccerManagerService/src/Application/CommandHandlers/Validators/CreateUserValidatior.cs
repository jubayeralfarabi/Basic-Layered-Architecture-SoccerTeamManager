using FluentValidation;
using Soccer.Domain.Entities;

namespace Soccer.Application.CommandHandlers.Validators
{
    public class CreateUserValidatior : AbstractValidator<Users>
    {
        public CreateUserValidatior()
        {
            //this.RuleFor(u => u.CustomerId).NotEmpty().WithMessage("Invalid CustomerId");
            //this.RuleFor(u => u.AlterationDetails).NotNull().WithMessage("AlterationDetails must have an element");
            //When(u => u.AlterationDetails != null, () => { 
            //    this.RuleFor(a => a.AlterationDetails.Length).GreaterThan(0).WithMessage("AlterationDetails must have an element");
            //});
            //this.RuleFor(u => u.AlterationId).NotEqual(Guid.Empty).WithMessage("Invalid AlterationId");
        }
    }
}
