using ErrorOr;

using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Authentication.Common;

namespace BuberDinner.Application.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}

