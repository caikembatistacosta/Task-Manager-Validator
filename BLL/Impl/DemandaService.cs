using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.Demandas;
using Shared;
using DataAccessLayer.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Impl
{
    public class DemandaService : IDemandaService
    {
        private readonly IUnitOfWork unitOfWork;

        public DemandaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response> Insert(Demanda Demanda)
        {
            Response response = new DemandaInsertValidator().Validate(Demanda).ConvertToResponse();

            if (!response.HasSuccess)
            {
                return response;
            }

            response = await unitOfWork.DemandaDAO.Insert(Demanda);
            if (response.HasSuccess)
            {
                await unitOfWork.DemandaDAO.Insert(Demanda);
                return response;
            }
            return response;
        }

        public async Task<Response> Update(Demanda Demanda)
        {
            Response response = new DemandaUpdateValidator().Validate(Demanda).ConvertToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }

            response = await unitOfWork.DemandaDAO.Update(Demanda);
            if (response.HasSuccess)
            {
                await unitOfWork.Commit();
                return response;
            }
            return response;
        }
        public async Task<Response> ChangeStatusInProgress(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await unitOfWork.DemandaDAO.GetById(Demanda.ID);

            if (Demanda == null)
            {
                return singleResponse;
            }
            Demanda.StatusDaDemanda = Entities.Enums.StatusDemanda.Andamento;
            Response response = await unitOfWork.DemandaDAO.Update(Demanda);
            if (response.HasSuccess)
            {
                await unitOfWork.Commit();
                return response;
            }
            return response;
        }
        public async Task<Response> ChangeStatusInFinished(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await unitOfWork.DemandaDAO.GetById(Demanda.ID);

            if (Demanda == null)
            {
                return singleResponse;
            }
            Demanda.StatusDaDemanda = Entities.Enums.StatusDemanda.Finalizada;

            Response response = await unitOfWork.DemandaDAO.UpdateStatus(Demanda);
            if (response.HasSuccess)
            {
                await unitOfWork.Commit();
                return response;
            }
            return response;
        }
        public async Task<DataResponse<Demanda>> GetAll()
        {
            return await unitOfWork.DemandaDAO.GetAll();
        }
        public async Task<SingleResponse<Demanda>> GetById(int id)
        {
            return await unitOfWork.DemandaDAO.GetById(id);
        }
        public async Task<DataResponse<Demanda>> GetLast6()
        {
            return await unitOfWork.DemandaDAO.GetLast6();
        }
    }
}
