//using ErrorOr;
//using AErrors = BuberDinner.Application.Common.Errors.Errors;
//using BuberDinner.Application.Common.Interfaces.Authentication;
//using BuberDinner.Application.Common.Interfaces.Persistence;
//using BuberDinner.Domain.Entities;
//using BuberDinner.Domain.Common.Errors;
//using BuberDinner.Application.Services.Authentication.Common;

//namespace BuberDinner.Application.Services.Authentication.Querires;

//public class AuthenticationQueryService : IAuthenticationQueryService
//{
//    private readonly IJwtTokenGenerator _jwtTokenGenerator;
//    private readonly IUserRepository _userRepository;
//    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
//    {
//        _jwtTokenGenerator = jwtTokenGenerator;
//        _userRepository = userRepository;
//    }
//    public ErrorOr<AuthenticationResult> Login(string email, string password)
//    {
//        //validate the user exists
//        if (_userRepository.GetUserByEmail(email) is not User user)
//            return AErrors.Authentication.InvalidCredentials;

//        //validate the password is correct
//        if (user.Password != password)
//            return AErrors.Authentication.InvalidCredentials;

//        //create jwt token
//        var token = _jwtTokenGenerator.GenerateToken(user);

//        return new AuthenticationResult(
//            user,
//            token
//            );
//    }
//}
