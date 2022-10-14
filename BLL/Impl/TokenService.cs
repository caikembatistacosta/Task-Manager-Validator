using BusinessLogicalLayer.Interfaces;
using Shared;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace BusinessLogicalLayer.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILog log;
        public TokenService(IUnitOfWork unitOfWork, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.log = log;
        }

        public async Task<Response> DeleteRefreshToken(string email, string refreshToken)
        {
            Response response = await unitOfWork.TokenDAO.DeleteRefreshToken(email, refreshToken);
            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora",response.Exception);
                    return response;
                }
                log.Error("Uma exceção foi gerada",response.Exception);
                return response;
            }
            response = await unitOfWork.Commit();
            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora", response.Exception);
                    return response;
                }
                log.Error("Uma exceção foi gerada", response.Exception);
                return response;
            }
            log.Info("Sucesso ao deletar o RefreshToken");
            return response;
        }
        public async Task<SingleResponse<Funcionario>> GetRefreshToken(string email)
        {
            SingleResponse<Funcionario> single = await unitOfWork.TokenDAO.GetRefreshToken(email);
            if (!single.HasSuccess)
            {
                if (single.Exception != null)
                {
                    if (single.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora", single.Exception);
                        return single;
                    }
                    log.Error("Uma exceção foi gerada", single.Exception);
                    return single;
                }
                log.Warn("Erro ao buscar o RefreshToken");
                return single;
            }
            log.Info("Sucesso ao pegar o RefreshToken");
            return single;
        }

        public async Task<SingleResponse<Funcionario>> InsertRefreshToken(string email, string refreshToken)
        {
            log.Debug("Tentando inserir o RefreshToken");
            SingleResponse<Funcionario> singleResponse = await unitOfWork.TokenDAO.InsertRefreshToken(email, refreshToken);
            if (singleResponse.HasSuccess)
            {
                Response response = await unitOfWork.Commit();
                if (response.Exception != null)
                {
                    if (response.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O Banco está fora", response.Exception);
                        singleResponse.HasSuccess = false;
                        return singleResponse;
                    }
                    log.Error("Uma exceção foi gerada", response.Exception);
                    singleResponse.HasSuccess = false;
                    return singleResponse;
                }
                log.Info("Sucesso ao criar um RefreshToken");
                return singleResponse;
            }
            if (singleResponse.Exception != null)
            {
                if (singleResponse.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora",singleResponse.Exception);
                    return singleResponse;
                }
                log.Error("Uma exceção foi gerada",singleResponse.Exception);
                return singleResponse;
            }
            log.Warn("Erro ao pegar o funcionario");
            return singleResponse;
        }
        public SingleResponse<string> GenerateToken(Funcionario funcionario)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, funcionario.Email),
                    new Claim(ClaimTypes.Role, funcionario.NivelDeAcesso.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            if (token == null)
            {
                return SingleResponseFactory<string>.CreateInstance().CreateFailureSingleResponse();
            }
            return SingleResponseFactory<string>.CreateInstance().CreateSuccessSingleResponse(tokenHandler.WriteToken(token));
        }

        public SingleResponse<string> GenerateToken(IEnumerable<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            if (token == null)
            {
                return SingleResponseFactory<string>.CreateInstance().CreateFailureSingleResponse();
            }
            return SingleResponseFactory<string>.CreateInstance().CreateSuccessSingleResponse(tokenHandler.WriteToken(token));
        }

        public SingleResponse<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                TokenValidationParameters tokenValidationParams = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret.PadRight(512 / 8, '\0'))),
                    ValidateLifetime = false,
                };
                JwtSecurityTokenHandler tokenHandler = new();
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return SingleResponseFactory<ClaimsPrincipal>.CreateInstance().CreateFailureSingleResponse("Token inválido");
                }
                return SingleResponseFactory<ClaimsPrincipal>.CreateInstance().CreateSuccessSingleResponse(principal);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<ClaimsPrincipal>.CreateInstance().CreateFailureSingleResponse(ex);
            }

        }
        public SingleResponse<string> RefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rgn = RandomNumberGenerator.Create();
            rgn.GetBytes(randomNumber);
            return SingleResponseFactory<string>.CreateInstance().CreateSuccessSingleResponse(Convert.ToBase64String(randomNumber));
        }

        public Response ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SingleResponse<TokenValidationParameters> validationParameters = GetValidationParameters();
            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters.Item, out SecurityToken validatedToken);
            if (!principal.Identity.IsAuthenticated || principal.Identity.Name == null)
            {
                return new Response()
                {
                    HasSuccess = false,
                    Message = "Token validado com sucesso"
                };
            }

            return new Response()
            {
                HasSuccess = true,
                Message = "Token validado com sucesso"
            };
        }

        public SingleResponse<TokenValidationParameters> GetValidationParameters()
        {
            var token = new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)) // The same key as the one that generate the token
            };
            return new SingleResponse<TokenValidationParameters>()
            {
                HasSuccess = true,
                Message = "Sucesso",
                Item = token
            };
        }
    }
}
