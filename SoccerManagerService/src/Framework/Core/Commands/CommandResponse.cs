namespace Soccer.Platform.Infrastructure.Core.Commands
{
    using Soccer.Platform.Infrastructure.Core.Validation;

    public class CommandResponse
    {
        public CommandResponse()
        {
            this.ValidationResult = new ValidationResponse();
        }

        public CommandResponse(ValidationResponse validationResult, object result)
        {
            this.ValidationResult = validationResult;
            this.Result = result;
        }

        public ValidationResponse ValidationResult { get; set; }

        public object Result { get; set; }
    }
}
