﻿using ErrorOr;
using MediatR;
using FluentValidation;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidateBehavior<TRequest, TResponse> :
   IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;
    public ValidateBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));
        //khong co cach nao chuyen errors nay => TRquest
        return (dynamic)errors;
    }
}
