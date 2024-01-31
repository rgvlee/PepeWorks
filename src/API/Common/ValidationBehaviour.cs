using FluentResults;
using FluentValidation;
using MediatR;

namespace API.Common;

public class ValidationBehaviour<TRequest, TResultValue> : IPipelineBehavior<TRequest, Result<TResultValue>>
    where TRequest : IRequest<Result<TResultValue>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Result<TResultValue>> Handle(TRequest request, RequestHandlerDelegate<Result<TResultValue>> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        return validationResults.Any(x => !x.IsValid)
            ? Result.Fail<TResultValue>(validationResults
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .Select(x => new ValidationError(x)))
            : await next();
    }
}