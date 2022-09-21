using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDemandaService
    {
        Task<Response> Insert(Demanda Demanda);
        Task<Response> Update(Demanda Demanda);
        Task<DataResponse<Demanda>> GetAll();
        Task<SingleResponse<Demanda>> GetById(int id);
        Task<Response> ChangeStatusInProgress(Demanda Demanda);
    }
}
