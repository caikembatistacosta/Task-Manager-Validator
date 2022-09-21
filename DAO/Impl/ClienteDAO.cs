using Common;
using DAO.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Impl
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly DemandasDbContext _db;
        public ClienteDAO(DemandasDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Cliente cliente)
        {
            _db.Enderecos.Add(cliente.Endereco);
            _db.Clientes.Add(cliente);
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

        public async Task<Response> Update(Cliente cliente)
        {
            Cliente? clienteDB = await _db.Clientes.Include(c => c.Endereco).Include(c => c.Endereco.Estado).FirstOrDefaultAsync(c => c.ID == cliente.ID);
            if (clienteDB == null)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse();
            }
            clienteDB.Nome = cliente.Nome;
            clienteDB.Email = cliente.Email;
            clienteDB.Endereco.Bairro = cliente.Endereco.Bairro;
            clienteDB.Endereco.Rua = cliente.Endereco.Rua;
            clienteDB.Endereco.Cep = cliente.Endereco.Cep;
            clienteDB.Endereco.Cidade = cliente.Endereco.Cidade;
            clienteDB.Endereco.Estado.UF = cliente.Endereco.Estado.UF;
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

        public async Task<Response> Delete(Cliente cliente)
        {
            _db.Clientes.Remove(cliente);

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
        public async Task<DataResponse<Cliente>> GetAll()
        {
            try
            {
                List<Cliente> clientes = await _db.Clientes.Include(c => c.Endereco).Include(c => c.Endereco.Estado).ToListAsync();
                if (clientes.Count < 0)
                {
                    return DataResponseFactory<Cliente>.CreateInstance().CreateFailureDataResponse();
                }
               
                return DataResponseFactory<Cliente>.CreateInstance().CreateSuccessDataResponse(clientes);
               
            }
            catch (Exception ex)
            {
                return DataResponseFactory<Cliente>.CreateInstance().CreateFailureDataResponse(ex);
            }
        }
        public async Task<SingleResponse<Cliente>> GetById(int id)
        {
            try
            {
                Cliente item = await _db.Clientes.Include(c => c.Endereco).Include(c => c.Endereco.Estado).FirstOrDefaultAsync(c => c.ID == id);
                return SingleResponseFactory<Cliente>.CreateInstance().CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Cliente>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
    }
}
