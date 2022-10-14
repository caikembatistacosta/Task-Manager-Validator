using Shared;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Impl
{
    public class FuncionarioDAO : IFuncionarioDAO
    {
        private readonly DemandasDbContext _db;
        public FuncionarioDAO(DemandasDbContext db)
        {
            this._db = db;
        }
        public async Task<Response> Insert(Funcionario funcionario)
        {
            try
            {
                _db.Enderecos.Add(funcionario.Endereco);
                _db.Funcionarios.Add(funcionario);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }

        public async Task<Response> Update(Funcionario funcionario)
        {
            Funcionario? funcionario1 = await _db.Funcionarios.Include(x => x.Endereco).Include(x => x.Endereco.Estado).FirstOrDefaultAsync(x => x.ID == funcionario.ID);
            if (funcionario1 == null)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse();
            }
            funcionario1 = funcionario;
            try
            {
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }
        public async Task<Response> Delete(Funcionario funcionario)
        {
            Funcionario funcionario1 = await _db.Funcionarios.FindAsync(funcionario.ID);
            _db.Funcionarios.Remove(funcionario1);
            try
            {
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
                List<Funcionario> funcionarios = await _db.Funcionarios.Include(c => c.Endereco).Include(c => c.Endereco.Estado).AsNoTracking().ToListAsync();
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
                Funcionario? item = await _db.Funcionarios.Include(c => c.Endereco).Include(c => c.Endereco.Estado).FirstOrDefaultAsync(c => c.ID == id);
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
