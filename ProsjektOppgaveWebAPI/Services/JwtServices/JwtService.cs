using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Services.JwtServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.JwtServices;

public class JwtService: IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptionsMonitor<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.CurrentValue;
    }
    
    public async Task<ResponseService<string>> GenerateToken(IdentityUser user)
    {
        var byteKey = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        var securityKey = new SymmetricSecurityKey(byteKey);
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = await GetClaims(user);

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtOptions.ExpirationHours),
            signingCredentials: credentials);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            return ResponseService<string>.Ok(tokenHandler.WriteToken(jwtToken));
        }
        catch (Exception e)
        {
            return ResponseService<string>.Error(Errors.GENERATE_JWT_TOKEN_ERROR);
        }
    }

    public async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        return new List<Claim>()
        {
            new Claim("Id", user.Id),
            new Claim("Name", user.UserName)
        };
    }
}