using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemandasDbContext _context;
        private IFuncionarioDAO funcionario = null;
        private IDemandaDAO demanda = null;
        private ITokenDAO tokenDAO = null;
        public UnitOfWork(DemandasDbContext demandasDb, IFuncionarioDAO funcionario, IDemandaDAO demandaDAO)
        {
            _context = demandasDb;
            this.funcionario = funcionario;
            demanda = demandaDAO;
        }
        /// <summary>
        /// Salvando as alterações no banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailureResponse(ex);
            }
        }
        /// <summary>
        /// Atribuindo o contexto ao FuncionarioDAO.
        /// </summary>
        public IFuncionarioDAO FuncionarioDAO
        {
            get
            {
                if (funcionario == null)
                {
                    funcionario = new FuncionarioDAO(_context);
                }
                return funcionario;
            }
        }
        /// <summary>
        /// Atribuindo o contexto ao FuncionarioDAO.
        /// </summary>
        public ITokenDAO TokenDAO
        {
            get
            {
                if (tokenDAO == null)
                {
                    tokenDAO = new TokenDAO(_context);
                }
                return tokenDAO;
            }
        }
        /// <summary>
        /// Atribuindo o contexto ao FuncionarioDAO.
        /// </summary>
        public IDemandaDAO DemandaDAO
        {
            get
            {
                if (demanda == null)
                {
                    demanda = new DemandaDAO(_context);
                }
                return demanda;
            }
        }
        /// <summary>
        /// Método dispose que é chamado para garantir o fechamento do banco, é chamado automaticamente.
        /// </summary>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
