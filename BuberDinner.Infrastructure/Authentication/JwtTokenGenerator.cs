using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Domain.UserAggregate;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSetting;
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSetting = jwtOptions.Value;
    }
    public string GenerateToken(User user)
    {
        //SymmetricSecurityKey tao ra tu mot chuoi bi mat
        //chi dinh thuat toan ma hoa dung de ky token
        var sigiingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSetting.Secret)),
                SecurityAlgorithms.HmacSha256);
        //cung cap cac hang so dai dien cho cac claim pho bien su dung trong jwt
        //jti jwt id, mot dinh danh cho token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
            claims: claims,
            signingCredentials: sigiingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
