using Common;
using DAO.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Impl
{
    public class FuncionarioDAO : IFuncionarioDAO
    {
        private readonly DemandasDbContext _db;
        private readonly ITokenDAO token;
        public FuncionarioDAO(DemandasDbContext db, ITokenDAO token)
        {
            this._db = db;
            this.token = token;
        }
        public async Task<Response> Insert(Funcionario funcionario)
        {
            _db.Funcionarios.Add(funcionario);

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

        public async Task<Response> Update(Funcionario funcionario)
        {
            _db.Funcionarios.Update(funcionario);
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
        public async Task<Response> Delete(Funcionario funcionario)
        {
            _db.Funcionarios.Remove(funcionario);
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

        public async Task<DataResponse<Funcionario>> GetAll()
        {

            try
            {
                List<Funcionario> funcionarios = await _db.Funcionarios.ToListAsync();
                return DataResponseFactory<Funcionario>.CreateInstance().CreateSuccessDataResponse(funcionarios);
            }
            catch (Exception ex)
            {
                return DataResponseFactory<Funcionario>.CreateInstance().CreateFailureDataResponse(ex);
            }
        }

        public async Task<SingleResponse<Funcionario>> GetById(int id)
        {
            try
            {
                Funcionario item = await _db.Funcionarios.FindAsync(id);
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }

        public async Task<SingleResponse<Funcionario>> GetLogin(Funcionario funcionario)
        {
            try
            {
                Funcionario? funcionario1 = await _db.Funcionarios.FirstOrDefaultAsync(f => f.Email == funcionario.Email && f.Senha == funcionario.Senha);
                if (funcionario1 == null)
                {
                    return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse();
                }
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateSuccessSingleResponse(funcionario1);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Funcionario>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
    }
}
