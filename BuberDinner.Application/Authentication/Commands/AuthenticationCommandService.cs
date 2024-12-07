//using ErrorOr;

//using AErrors = BuberDinner.Application.Common.Errors.Errors;
//using BuberDinner.Application.Common.Interfaces.Authentication;
//using BuberDinner.Application.Common.Interfaces.Persistence;
//using BuberDinner.Domain.Entities;
//using BuberDinner.Domain.Common.Errors;
//using BuberDinner.Application.Services.Authentication.Common;

//namespace BuberDinner.Application.Services.Authentication.Commands;

//public class AuthenticationCommandService : IAuthenticationCommandService
//{
//    private readonly IJwtTokenGenerator _jwtTokenGenerator;
//    private readonly IUserRepository _userRepository;
//    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
//    {
//        _jwtTokenGenerator = jwtTokenGenerator;
//        _userRepository = userRepository;
//    }
//    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
//    {
//        //check if user already exists
//        if (_userRepository.GetUserByEmail(email) is not null)
//            return Errors.User.DuplicateEmail;
//        //create user
//        var user = new User
//        {
//            Email = email,
//            FirstName = firstName,
//            LastName = lastName,
//            Password = password
//        };
//        _userRepository.Add(user);
//        //create jwt token
//        var token = _jwtTokenGenerator.GenerateToken(user);

//        return new AuthenticationResult(
//                user,
//                token
//                );
//    }
//}
