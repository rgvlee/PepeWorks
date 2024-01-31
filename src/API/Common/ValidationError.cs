using FluentResults;
using FluentValidation.Results;

namespace API.Common;

public class ValidationError : Error
{
    public ValidationError(ValidationFailure validationFailure)
    {
        Message = validationFailure.ErrorMessage;
        Metadata.Add(nameof(validationFailure.PropertyName), validationFailure.PropertyName);
    }
}