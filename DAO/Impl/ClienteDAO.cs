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
            _db.Clientes.Add(cliente);

            try
            {
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }

        public async Task<Response> Update(Cliente cliente)
        {
            Cliente clienteDB = await _db.Clientes.FindAsync(cliente.ID);
            clienteDB.Nome = cliente.Nome;
            clienteDB.Email = cliente.Email;
            try
            {
                

                await _db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }

        public async Task<Response> Delete(Cliente cliente)
        {
            _db.Clientes.Remove(cliente);

            try
            {
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }
        public async Task<DataResponse<Cliente>> GetAll()
        {
            try
            {
                List<Cliente> clientes = await _db.Clientes.ToListAsync();
                return DataResponseFactory<Cliente>.CreateSuccessDataResponse(clientes);

            }
            catch (Exception ex)
            {
                return DataResponseFactory<Cliente>.CreateFailureDataResponse(ex);
            }
        }
        public async Task<SingleResponse<Cliente>> GetById(int id)
        {
            try
            {
                var item = await _db.Clientes.FindAsync(id);
                return SingleResponseFactory<Cliente>.CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Cliente>.CreateFailureSingleResponse(ex);
            }
        }
    }
}
