
using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.Funcionarios;
using DataAccessLayer.Interfaces;
using Entities;
using log4net;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicalLayer.Impl
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FuncionarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> Insert(Funcionario funcionario)
        {
            log.Debug("Tentando validar os dados do usuário");
            Response response = new FuncInsertValidator().Validate(funcionario).ConvertToResponse();
            if (!response.HasSuccess)
            {
                log.Warn($"Ocorreu um erro na validação {response.Message}");
                return response;
            }
            log.Debug("Tentando inserir o funcionario");
            response = await _unitOfWork.FuncionarioDAO.Insert(funcionario);
            if (response.HasSuccess)
            {
                log.Info("Sucesso ao inserir o funcionário no banco");
                response = await _unitOfWork.Commit();
                if (response.Exception != null)
                {
                    if (response.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora", response.Exception);
                        return response;
                    }
                    log.Error("Erro ao salvar o funcionário no banco", response.Exception);
                    return response;
                }
                log.Info("Sucesso ao salvar o funcionário no banco");
                return response;
            }

            log.Warn("Aviso, erro ao inserir o funcionário no banco", response.Exception);
            return response;
        }

        public async Task<Response> Update(Funcionario funcionario)
        {
            log.Debug("Tentando validar o funcionario");
            Response response = new FuncInsertValidator().Validate(funcionario).ConvertToResponse();
            if (!response.HasSuccess)
            {
                log.Warn($"Erro na validação do funcionário: {response.Message}");
                return response;
            }
            log.Debug("Atualizando o funcionário do banco");
            response = await _unitOfWork.FuncionarioDAO.Update(funcionario);
            if (!response.HasSuccess)
            {
                if (response.Exception != null)
                {
                    if (response.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora do ar", response.Exception);
                        return response;
                    }
                    log.Error("Uma exceção na hora de dar update", response.Exception);
                    return response;
                }
                log.Warn($"Erro ao atualizar o funcionário {response.Message}");
                return response;
            }
            log.Debug("Salvando as atualizações do funcionário no banco");
            response = await _unitOfWork.Commit();
            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora do ar");
                    return response;
                }
                log.Error("Erro na hora de salvar os dados atualizados", response.Exception);
                return response;
            }
            log.Info("Sucesso ao atualizar o funcionário");
            return response;
        }

        public async Task<Response> Delete(Funcionario funcionario)
        {
            log.Debug("Validando o funcionário");
            Response response = new FuncInsertValidator().Validate(funcionario).ConvertToResponse();
            if (!response.HasSuccess)
            {
                log.Warn($"A validação falhou {response.Message}");
                return response;
            }
            log.Debug("Tentando deletar um funcionário");
            response = await _unitOfWork.FuncionarioDAO.Delete(funcionario);
            if (!response.HasSuccess)
            {
                if (response.Exception != null)
                {
                    if (response.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O Banco está fora do ar", response.Exception);
                        return response;
                    }
                    log.Error("Erro na hora de deletar do banco", response.Exception);
                    return response;
                }
                log.Warn($"Algum erro de dados passados ocorreram na hora de deletar: {response.Message}");
                return response;
            }
            response = await _unitOfWork.Commit();
            if (response.Exception != null)
            {
                if (response.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("O banco está fora do ar");
                    return response;
                }
                log.Error("Exceção lançada na hora de salvar no banco", response.Exception);
                return response;
            }
            log.Info($"Sucesso ao deletar o funcionário");
            return response;
        }

        public async Task<DataResponse<Funcionario>> GetAll()
        {
            log.Debug("Efetuando a busca de todos os funcionários");
            DataResponse<Funcionario> data = await _unitOfWork.FuncionarioDAO.GetAll();
            if (data.Exception != null)
            {
                if (data.Exception.Message.Contains("Timeout"))
                {
                    log.Fatal("Timeout");
                    return data;
                }
                log.Error("Exceção gerada ao tentar listar todos os funcionários", data.Exception);
                return data;
            }
            log.Info("Sucesso ao listar todos os fucnionários");
            return data;
        }

        public async Task<SingleResponse<Funcionario>> GetById(int id)
        {
            log.Debug("Efetuando a busca do funcionário pelo id");
            SingleResponse<Funcionario> single = await _unitOfWork.FuncionarioDAO.GetById(id);
            if (!single.HasSuccess)
            {
                if (single.Exception != null)
                {
                    if (single.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora do ar",single.Exception);
                        return single;
                    }
                    log.Error("Exceção gerada na hora de pegar o funcionário",single.Exception);
                    return single;
                }
                log.Warn($"Erro ao tentar pegar o funcionário pelo id: {id}");
                return single;
            }
            log.Info("Sucesso ao buscar o funcionário pelo id");
            return single;
        }

        public async Task<SingleResponse<Funcionario>> GetLogin(Funcionario funcionario)
        {
            log.Debug("Efetuando a busca do login");
            SingleResponse<Funcionario> single = await _unitOfWork.FuncionarioDAO.GetLogin(funcionario);
            if (!single.HasSuccess)
            {
                if (single.Exception != null)
                {
                    if (single.Exception.Message.Contains("Timeout"))
                    {
                        log.Fatal("O banco está fora do ar", single.Exception);
                        return single;
                    }
                    log.Error("Exceção gerada na hora de pegar o login",single.Exception);
                    return single;
                }
                log.Warn("Erro ao retornar o login");
                return single;
            }
            log.Info("Sucesso na busca pelo login");
            return single;
        }
    }
}
