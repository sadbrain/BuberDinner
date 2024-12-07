﻿using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ErrorOr;
using System.Reflection;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Behaviors;
using FluentValidation;
namespace BuberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        //services.AddScoped<
        //    IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>,
        //    ValidateRegisterCommandBehavior>()
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}
