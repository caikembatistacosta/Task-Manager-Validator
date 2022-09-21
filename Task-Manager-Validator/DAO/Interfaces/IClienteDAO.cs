using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IClienteDAO
    {
        Task<Response> Insert(Cliente cliente);
        Task<Response> Update(Cliente cliente);
        Task<Response> Delete(Cliente cliente);
        Task<DataResponse<Cliente>> GetAll();
        Task<SingleResponse<Cliente>> GetById(int id);
    }
}
