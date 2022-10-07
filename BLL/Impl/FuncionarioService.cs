
using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.Funcionarios;
using DataAccessLayer.Interfaces;
using Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Impl
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FuncionarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> Insert(Funcionario funcionario)
        {
            Response response = new FuncInsertValidator().Validate(funcionario).ConvertToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }
            Response responsee = await _unitOfWork.FuncionarioDAO.Insert(funcionario);
            if (responsee.HasSuccess)
            {
                await _unitOfWork.Commit();
                return responsee;
            }
            return response;
        }

        public async Task<Response> Update(Funcionario funcionario)
        {
            Response response = new FuncInsertValidator().Validate(funcionario).ConvertToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }

            Response responsee = await _unitOfWork.FuncionarioDAO.Update(funcionario);
            if (responsee.HasSuccess)
            {
                await _unitOfWork.Commit();
                return responsee;
            }
            return response;
        }
        
    public async Task<Response> Delete(Funcionario funcionario)
    {
        Response response = new FuncInsertValidator().Validate(funcionario).ConvertToResponse();
        if (!response.HasSuccess)
        {
            return response;
        }
        Response responses = await _unitOfWork.FuncionarioDAO.Delete(funcionario);
        if (responses.HasSuccess)
        {
            await _unitOfWork.Commit();
            return response;
        }
        return response;
    }

    public async Task<DataResponse<Funcionario>> GetAll()
    {
        return await _unitOfWork.FuncionarioDAO.GetAll();
    }

    public async Task<SingleResponse<Funcionario>> GetById(int id)
    {
        return await _unitOfWork.FuncionarioDAO.GetById(id);
    }

    public async Task<SingleResponse<Funcionario>> GetLogin(Funcionario funcionario)
    {
        return await _unitOfWork.FuncionarioDAO.GetLogin(funcionario);
    }
}
}
