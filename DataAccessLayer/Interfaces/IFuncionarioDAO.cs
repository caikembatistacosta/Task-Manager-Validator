using Shared;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFuncionarioDAO
    {
        Task<Response> Insert(Funcionario funcionario);
        Task<Response> Update(Funcionario funcionario);
        Task<Response> Delete(Funcionario funcionario);
        Task<DataResponse<Funcionario>> GetAll();
        Task<SingleResponse<Funcionario>> GetById(int id);
        Task<SingleResponse<Funcionario>> GetLogin(Funcionario funcionario);
        Task<SingleResponse<Funcionario>> GetByEmail(Funcionario funcionario);
    }
}
