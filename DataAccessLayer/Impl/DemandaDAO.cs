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
        /// <summary>
        /// Inserindo a demanda no banco de dados.
        /// </summary>
        /// <param name="Demanda"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Atualizando os dados da demanda.
        /// </summary>
        /// <param name="Demandas"></param>
        /// <returns></returns>
        public async Task<Response> Update(Demanda Demandas)
        {
            Demanda? DemandaDB = await _db.Demandas.FindAsync(Demandas.ID);
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
        /// <summary>
        /// Listando todas as demandas.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Pegando uma única demanda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Pegando as últimas 6 demandas cadastradas.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Atualizando os status da demanda selecionada.
        /// </summary>
        /// <param name="demanda"></param>
        /// <returns></returns>
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
