using Shared;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DataAccessLayer.Impl
{
    public class TokenDAO : ITokenDAO
    {
        private readonly DemandasDbContext _db;
        public TokenDAO(DemandasDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Inserindo o refresh token no banco no usuário.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateSuccessSingleResponse(funcionario);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        /// <summary>
        /// Pegando o refresh token.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Apagando o refresh token.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
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
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }

      


    }
}
