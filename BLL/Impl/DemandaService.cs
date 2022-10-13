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

namespace BusinessLogicalLayer.Impl
{
    public class DemandaService : IDemandaService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DemandaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response> Insert(Demanda Demanda)
        {
            Response response = new DemandaInsertValidator().Validate(Demanda).ConvertToResponse();

            if (response.HasSuccess)
            {
                if (response.Exception != null)
                {
                    
                    log.Error("Uma exceção foi gerada na criação de uma demanda", response.Exception);
                    return response;
                }
                log.Error("A validação não passou");
                return response;
            }
            response = await unitOfWork.DemandaDAO.Insert(Demanda);
            if (response.Exception != null)
            {
                log.Error("Uma exceção foi gerada", response.Exception);
                return response;
            }

            response = await unitOfWork.Commit();
            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora do ar", response.Exception);
                    return response;
                }
                log.Error($"Uma exceção foi gerada", response.Exception);
                return response;
            }
            log.Info("A demanda foi inserida com sucesso!");
            return response;
        }

        public async Task<Response> Update(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await unitOfWork.DemandaDAO.GetById(Demanda.ID);

            if (!singleResponse.HasSuccess)
            {
                if (singleResponse.Exception != null)
                {
                    if (singleResponse.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora do ar", singleResponse.Exception);
                        return singleResponse;
                    }
                    log.Error("Uma exceção foi gerada", singleResponse.Exception);
                    return singleResponse;
                }
                log.Info("Não foi possível achar a demanda");
                return singleResponse;
            }

            Response response = new DemandaUpdateValidator().Validate(Demanda).ConvertToResponse();
            if (!response.HasSuccess)
            {
                if (response.Exception != null)
                {
                    log.Error("Uma exceção foi gerada na validação", response.Exception);
                    return response;
                }
                log.Info("A validação falhou");
                return response;
            }

            response = await unitOfWork.DemandaDAO.Update(Demanda);
            if (response.Exception != null)
            {
                log.Error("Uma exceção foi gerada na hora de atualizar a demanda", response.Exception);
                return response;
            }
            response = await unitOfWork.Commit();

            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora do ar", response.Exception);
                    return response;
                }
                log.Error("Uma exeção foi gerada ao salvar o update da demanda", response.Exception);
                return response;
            }
            log.Info("A demanda foi atualizada com sucesso");
            return response;
        }
        public async Task<Response> ChangeStatusInProgress(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await unitOfWork.DemandaDAO.GetById(Demanda.ID);

            if (!singleResponse.HasSuccess)
            {
                if (singleResponse.Exception != null)
                {
                    if (singleResponse.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora do ar");
                        return singleResponse;
                    }
                    log.Error("Uma exceção foi gerada", singleResponse.Exception);
                    return singleResponse;
                }
                log.Info("Não foi possível achar essa demanda");
                return singleResponse;
            }
            Demanda.StatusDaDemanda = Entities.Enums.StatusDemanda.Andamento;
            Response response = new DemandaUpdateValidator().Validate(Demanda).ConvertToResponse();
            if (response.Exception != null)
            {
                log.Error("Uma exceção foi gerada", response.Exception);
                return response;
            }
            response = await unitOfWork.DemandaDAO.Update(Demanda);
            if (response.Exception != null)
            {
                log.Error("Uma exceção foi gerada", response.Exception);
                return response;
            }
            response = await unitOfWork.Commit();
            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora do ar");
                    return response;
                }
                log.Error("Uma exceção foi gerada");
                return response;
            }
            log.Info("Sucesso ao atualizar o status da demanda");
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
                        log.Fatal("Tempo excedido na query do banco", r.Exception);
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
            DataResponse<Demanda> data = await unitOfWork.DemandaDAO.GetAll();
            if (data.Exception != null)
            {
                if (data.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O Banco está fora", data.Exception);
                    return data;
                }
                log.Error("Uma exceção foi gerada", data.Exception);
                return data;
            }
            log.Info("Sucesso ao pegar todas as demandas");
            return data;
        }
        public async Task<SingleResponse<Demanda>> GetById(int id)
        {
            log.Info("Pegando a Demanda pelo ID");
            SingleResponse<Demanda> singleResponse = await unitOfWork.DemandaDAO.GetById(id);
            if (!singleResponse.HasSuccess)
            {
                if (singleResponse.Exception != null)
                {
                    if (singleResponse.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora", singleResponse.Exception);
                        return singleResponse;
                    }
                    log.Error("Uma exceção foi gerada", singleResponse.Exception);
                    return singleResponse;
                }
                log.Info("Não foi possível achar a demanda");
                return singleResponse;
            }
            log.Info("Demanda selecionada com sucesso");
            return singleResponse;
        }
        public async Task<DataResponse<Demanda>> GetLast6()
        {
            log.Info("Pegando as últimas 6 demandas");
            DataResponse<Demanda> data = await unitOfWork.DemandaDAO.GetLast6();
            if (data.Exception != null)
            {
                if (data.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora do ar", data.Exception);
                    return data;
                }
                log.Error("Uma exceção foi gerada", data.Exception);
                return data;
            }
            log.Info("Sucesso para pegar as últimas 6 demandas");
            return data;
        }
    }
}
