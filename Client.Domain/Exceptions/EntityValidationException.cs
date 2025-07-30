using Client.Domain.Validation.Common;

namespace Client.Domain.Exceptions
{
    public class EntityValidationException : Exception
    {
        public ValidationResult ValidationResult { get; }

        public EntityValidationException(ValidationResult result)
        {
            ValidationResult = result;
        }
    }
}
