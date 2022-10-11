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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                await unitOfWork.Commit();
                return response;
            }
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

            if (singleResponse.Item == null)
            {
                if (singleResponse.Exception != null)
                {
                    log.Error($"Lançada uma exception no ChangeStatusInFinished", singleResponse.Exception);
                    if (singleResponse.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("Exceção gerada, banco fora do ar", singleResponse.Exception);
                    }
                }
                log.Info("Não foi achada a demanda");
                return singleResponse;
            }
            Demanda.StatusDaDemanda = Entities.Enums.StatusDemanda.Finalizada;

            Response response = await unitOfWork.DemandaDAO.UpdateStatus(Demanda);
            if (!response.HasSuccess)
            {
                if (response.Exception != null)
                {
                    log.Error("Uma exceção foi gerada", response.Exception);
                    if (response.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O Banco está fora", response.Exception);
                    }
                }
                return response;
            }
            log.Info("Tentativa de salvar os dados no Banco de Dados");
            Response r = await unitOfWork.Commit();
            if (!r.HasSuccess)
            {
                if (r.Exception != null)
                {
                    log.Error("Uma exceção foi gerada", r.Exception);
                    if (r.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("Tempo excedido na query do banco",r.Exception);
                    }
                }
                return r;
            }
            log.Info("Sucesso na hora de efetuar o update");
            return r;
        }
        public async Task<DataResponse<Demanda>> GetAll()
        {
            log.Info("Pegando todas as demandas.");
            return await unitOfWork.DemandaDAO.GetAll();
        }
        public async Task<SingleResponse<Demanda>> GetById(int id)
        {
            log.Info("Pegando a Demanda pelo ID");
            return await unitOfWork.DemandaDAO.GetById(id);
        }
        public async Task<DataResponse<Demanda>> GetLast6()
        {
            log.Info("Pegando as últimas 6 demandas");
            return await unitOfWork.DemandaDAO.GetLast6();
        }
    }
}
