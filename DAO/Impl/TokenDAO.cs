using Common;
using DAO;
using DAO.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.ApiConfig
{
    public class TokenDAO : ITokenDAO
    {
        private readonly DemandasDbContext _db;
        public TokenDAO(DemandasDbContext db)
        {
            _db = db;
        }

        public async Task<SingleResponse<Funcionario>> InsertRefreshToken(string email, string token)
        {
            Funcionario? funcionario = _db.Funcionarios.FirstOrDefault(x => x.Email == email);
            if (funcionario == null)
            {
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse();
            }
            funcionario.RefreshToken = token;
            try
            {
                await _db.SaveChangesAsync();
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateSuccessSingleResponse(funcionario);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        public async Task<SingleResponse<Funcionario>> GetRefreshToken(string email)
        {
            try
            {
                Funcionario? funcionario = await _db.Funcionarios.FirstOrDefaultAsync(c => c.Email == email);
                if (funcionario == null)
                    return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse();

                return SingleResponseFactory<Funcionario>.CreateInstance().CreateSuccessSingleResponse(funcionario);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        public async Task<Response> DeleteRefreshToken(string email, string refreshToken)
        {
            Funcionario? funcionario = _db.Funcionarios.FirstOrDefault(c => c.Email == email);
            if (funcionario == null)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse();
            }
            funcionario.RefreshToken = null;
            try
            {
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }

      


    }
}
