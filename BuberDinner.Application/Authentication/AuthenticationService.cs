using ErrorOr;

using AErrors = BuberDinner.Application.Common.Errors.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.UserAggregate;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Application.Authentication.Common;

namespace BuberDinner.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        throw new NotImplementedException();

        ////validate the user exists
        //if (_userRepository.GetUserByEmail(email) is not User user)
        //    return AErrors.Authentication.InvalidCredentials;

        ////validate the password is correct
        //if (user.Password != password)
        //    return AErrors.Authentication.InvalidCredentials;

        ////create jwt token
        //var token = _jwtTokenGenerator.GenerateToken(user);

        //return new AuthenticationResult(
        //    user,
        //    token
        //    );
    }
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        throw new NotImplementedException();
        ////check if user already exists
        //if (_userRepository.GetUserByEmail(email) is not null)
        //    return Errors.User.DuplicateEmail;
        ////create user
        //var user = new User
        //{
        //    Email = email,
        //    FirstName = firstName,
        //    LastName = lastName,
        //    Password = password
        //};
        //_userRepository.Add(user);
        ////create jwt token
        //var token = _jwtTokenGenerator.GenerateToken(user);

        //return new AuthenticationResult(
        //        user,
        //        token
        //        );
    }
}
