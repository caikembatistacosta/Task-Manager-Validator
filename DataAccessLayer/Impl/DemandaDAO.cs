using Shared;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Impl
{
    public class DemandaDAO : IDemandaDAO
    {
        private readonly DemandasDbContext _db;
        public DemandaDAO(DemandasDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Demanda Demanda)
        {
         
            try
            {
                await _db.Demandas.AddAsync(Demanda);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }

        public async Task<Response> Update(Demanda Demandas)
        {
            Demanda? DemandaDB = await _db.Demandas.FindAsync(Demandas.ID);
            DemandaDB.ID = Demandas.ID;
            DemandaDB.Nome = Demandas.Nome;
            DemandaDB.DescricaoCurta = Demandas.DescricaoCurta;
            DemandaDB.DescricaoDetalhada = Demandas.DescricaoDetalhada;
            DemandaDB.DataFim = Demandas.DataFim;
            DemandaDB.StatusDaDemanda = Demandas.StatusDaDemanda;
            

            try
            {    
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }

        public async Task<Response> Delete(Demanda demanda)
        {
            _db.Demandas.Remove(demanda);
            try
            {
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }
        //Terminar delete
        public async Task<DataResponse<Demanda>> GetAll()
        {
            try
            {
                List<Demanda> Demandas = await _db.Demandas.OrderByDescending(c=> c.ID).ToListAsync();
                return DataResponseFactory<Demanda>.CreateInstance().CreateSuccessDataResponse(Demandas);

            }
            catch (Exception ex)
            {
                return DataResponseFactory<Demanda>.CreateInstance().CreateFailureDataResponse(ex);
            }
        }
        public async Task<SingleResponse<Demanda>> GetById(int id)
        {
            try
            {
                Demanda item = await _db.Demandas.FindAsync(id);
                if(id == null)
                {
                    return SingleResponseFactory<Demanda>.CreateInstance().CreateFailureSingleResponse();
                }
                return SingleResponseFactory<Demanda>.CreateInstance().CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Demanda>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }

        public async Task<DataResponse<Demanda>> GetLast6()
        {
            try
            {
                List<Demanda> Demandas = await _db.Demandas.OrderByDescending(c => c.ID).Take(6).ToListAsync();
                return DataResponseFactory<Demanda>.CreateInstance().CreateSuccessDataResponse(Demandas);

            }
            catch (Exception ex)
            {
                return DataResponseFactory<Demanda>.CreateInstance().CreateFailureDataResponse(ex);
            }
        }

        public async Task<Response> UpdateStatus(Demanda demanda)
        {
            Demanda DemandaDB = await _db.Demandas.FindAsync(demanda.ID);
            DemandaDB.ID = demanda.ID;
            DemandaDB.StatusDaDemanda = demanda.StatusDaDemanda;

            try
            { 
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }
    }
}
