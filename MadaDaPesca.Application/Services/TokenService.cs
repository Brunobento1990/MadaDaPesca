using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MadaDaPesca.Application.Services;

internal class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    private readonly SigningCredentials _credenciais;
    private readonly DateTime _expiresToken;
    private readonly DateTime _expiresRefreshToken;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        _credenciais = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        _expiresToken = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpireInMinutes"]!));
        _expiresRefreshToken = DateTime.UtcNow.AddDays(int.Parse(configuration["Jwt:ExpireRefreshInDays"]!));
    }

    public (string token, string refreshToken) GerarTokenGuiaDePesca(GuiaDePesca guiaDePesca)
    {
        var token = Gerar(new List<Claim>()
          {
            new("Id",guiaDePesca.Id.ToString()),
            new("DataDoLogin",DateTime.Now.ToString(_configuration["Format:Datetime"])),
            new("Guia","TRUE"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
          }, _expiresToken);

        var refreshToken = Gerar(new List<Claim>()
          {
            new("Id",guiaDePesca.Id.ToString()),
            new("Guia","TRUE"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
          }, _expiresRefreshToken);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

        return (token: tokenString, refreshToken: refreshTokenString);
    }

    private JwtSecurityToken Gerar(List<Claim> claims, DateTime expires)
    {
        return new JwtSecurityToken(
          issuer: _configuration["Jwt:Issue"]!,
          audience: _configuration["Jwt:Audience"]!,
          claims: claims,
          expires: expires,
          signingCredentials: _credenciais);
    }
}
