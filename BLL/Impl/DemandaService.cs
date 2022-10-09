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
using log4net;
using Microsoft.Extensions.Logging;

namespace BusinessLogicalLayer.Impl
{
    public class DemandaService : IDemandaService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<Demanda> logger;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DemandaService(IUnitOfWork unitOfWork, ILogger<Demanda> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task<Response> Insert(Demanda Demanda)
        {
            logger.LogInformation($"Tentando validar uma demanda");
            Response response = new DemandaInsertValidator().Validate(Demanda).ConvertToResponse();
            
            if (!response.HasSuccess)
            {
                logger.LogWarning("ERRO NA VALIDAÇÃO DA DEMANDA", response.Exception);
                return response;
            }
            logger.LogInformation("Tentando inserir uma demanda");
            response = await unitOfWork.DemandaDAO.Insert(Demanda);
            if (response.HasSuccess)
            {
                logger.LogInformation("Salvando no banco");
                await unitOfWork.Commit();
                return response;
            }
            logger.LogError("Erro na hora de inserir a demanda",response.Exception);
            return response;
        }

        public async Task<Response> Update(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await unitOfWork.DemandaDAO.GetById(Demanda.ID);

            if (Demanda == null)
            {
                return singleResponse;
            }

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
            logger.LogInformation("Tentando pegar todas as demandas");
            log.Info("Tentando pegar todas as demandas");
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
