using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IDemandaDAO
    {
        Task<Response> Insert(Demanda Demanda);
        Task<Response> Update(Demanda Demanda);
        Task<DataResponse<Demanda>> GetAll();
        Task<SingleResponse<Demanda>> GetById(int id);
    }
}
