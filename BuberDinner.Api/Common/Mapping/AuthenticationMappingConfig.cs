using Mapster;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;

namespace BuberDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(d => d.Token, s => s.Token)
            .Map(d => d.Id, s => s.User.Id.Value)
            .Map(d => d, s => s.User);
    }
}
