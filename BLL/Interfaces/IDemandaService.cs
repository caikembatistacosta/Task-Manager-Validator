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
        Task<DataResponse<Demanda>> GetLast6();
        Task<SingleResponse<Demanda>> GetById(int id);
        Task<Response> ChangeStatusInProgress(Demanda Demanda);
        Task<Response> ChangeStatusInFinished(Demanda Demanda);
        
    }
}
