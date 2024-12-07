using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest req)
    {
        //one of
        //OneOf<AuthenticationResult, IError> registerResult = _authenticationService.Register(
        //    req.FirstName,
        //    req.LastName,
        //    req.Email,
        //    req.Password);
        //return registerResult.Match(
        //    authResult => Ok(MapAuthResult(authResult)),
        //    error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));

        ////fluent result
        //Result<AuthenticationResult> registerResult = _authenticationService.Register(
        //    req.FirstName,
        //    req.LastName,
        //    req.Email,
        //    req.Password);
        //if (registerResult.IsSuccess) 
        //{
        //    return Ok(MapAuthResult(registerResult.Value));
        //}
        //var firstError = registerResult.Errors[0];
        //if(firstError is DuplicateEmailError)
        //{
        //    return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already exists");
        //}
        //return Problem();

        var command = _mapper.Map<RegisterCommand>(req);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var query = _mapper.Map<LoginQuery>(req);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);
        return authResult.Match(
           authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
           errors => Problem(errors));
    }
}
