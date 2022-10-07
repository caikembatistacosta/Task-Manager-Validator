using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IFuncionarioDAO FuncionarioDAO { get; }
        IDemandaDAO DemandaDAO { get; }
        Task<Response> Commit();
    }
}
