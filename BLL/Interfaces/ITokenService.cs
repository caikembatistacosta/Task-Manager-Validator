using Common;
using Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        SingleResponse<string> GenerateToken(Funcionario funcionario);
        SingleResponse<string> GenerateToken(IEnumerable<Claim> claims);
        SingleResponse<string> RefreshToken();
        SingleResponse<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task<SingleResponse<Funcionario>> InsertRefreshToken(string email, string refreshToken);
        Task<Response> DeleteRefreshToken(string email, string refreshToken);
        Task<SingleResponse<Funcionario>> GetRefreshToken(string email);
        Response ValidateToken(string token);
        SingleResponse<TokenValidationParameters> GetValidationParameters();
    }
}
