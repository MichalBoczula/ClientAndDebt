using Client.Domain.Validation.Common;

namespace Client.Domain.Validation.Abstract
{
    public interface IValidationRule<T>
    {
        void IsValid(T entity, ValidationResult validationResults);
    }
}
