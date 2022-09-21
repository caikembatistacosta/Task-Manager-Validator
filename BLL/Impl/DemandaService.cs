using BLL.Extensions;
using BLL.Interfaces;
using BLL.Validators.Demandas;
using Common;
using DAO.Impl;
using DAO.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class DemandaService : IDemandaService
    {
        private readonly IDemandaDAO DemandaDAO;

        public DemandaService(IDemandaDAO DemandaDAO)
        {
            this.DemandaDAO = DemandaDAO;
        }
        public async Task<DataResponse<Demanda>> GetAll()
        {
            return await DemandaDAO.GetAll();
        }
        public async Task<DataResponse<Demanda>> GetLast6()
        {
            return await DemandaDAO.GetLast6();
        }

        public async Task<SingleResponse<Demanda>> GetById(int id)
        {
            return await DemandaDAO.GetById(id);
        }

        public async Task<Response> Insert(Demanda Demanda)
        {
            Response response = new DemandaInsertValidator().Validate(Demanda).ConvertToResponse();
            //PetInsertValidator validator = new PetInsertValidator();
            //ValidationResult result = validator.Validate(p);
            //Response response = result.ConvertToResponse();

            if (!response.HasSuccess)
            //Se a validação não passou, retorne o response para tela!
            {
                return response;
            }
            //Se o pet está sendo cadastrado, então ele está ativo.

            //Se chegou aqui, é pq a validação passou e o PET está pronto pra ser cadastrado no banco.
            response = await DemandaDAO.Insert(Demanda);
            return response;
        }

        public async Task<Response> Update(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await DemandaDAO.GetById(Demanda.ID);

            if (Demanda == null)
            {
                return singleResponse;
            }

            Response response = new DemandaUpdateValidator().Validate(Demanda).ConvertToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }

            response = await DemandaDAO.Update(Demanda);
            return response;
        }
        public async Task<Response> ChangeStatusInProgress(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await DemandaDAO.GetById(Demanda.ID);
            
            if (Demanda == null)
            {
                return singleResponse;
            }
            Demanda.StatusDaDemanda = Entities.Enums.StatusDemanda.Andamento;

            return await DemandaDAO.UpdateStatus(Demanda);
            }
        public async Task<Response> ChangeStatusInFinished(Demanda Demanda)
        {
            SingleResponse<Demanda> singleResponse = await DemandaDAO.GetById(Demanda.ID);

            if (Demanda == null)
            {
                return singleResponse;
            }
            Demanda.StatusDaDemanda = Entities.Enums.StatusDemanda.Finalizada;

            return await DemandaDAO.UpdateStatus(Demanda);
        }
    }
}
