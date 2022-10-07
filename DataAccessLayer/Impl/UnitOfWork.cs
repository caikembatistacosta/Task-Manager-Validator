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
        public UnitOfWork(DemandasDbContext demandasDb, IFuncionarioDAO funcionario, IDemandaDAO demandaDAO)
        {
            _context = demandasDb;
            this.funcionario = funcionario;
            demanda = demandaDAO;
        }
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
        public IFuncionarioDAO FuncionarioDAO
        {
            get
            {
                if (funcionario ==null)
                {
                    funcionario = new FuncionarioDAO(_context);
                }
                return funcionario;
            }
        }
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
