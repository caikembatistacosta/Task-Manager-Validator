using Entities;
using BLL.Validators.ComonsValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Constants;

namespace BLL.Validators.Funcionarios
{
    internal class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        public void ValidateID()
        {
            RuleFor(c => c.Id).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_ID_VAZIO);
        }
        public void ValidateNome()
        {
            RuleFor(c => c.Nome).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                .MinimumLength(3).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_NOME_CURTO)
                                .MaximumLength(30).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_NOME_GRANDE);

            RuleFor(c => c.Sobrenome).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                     .MinimumLength(3).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_NOME_CURTO)
                                     .MaximumLength(30).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_NOME_GRANDE);
        }

        public void ValidateSenha()
        {
            RuleFor(c => c.Senha).NotNull().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_SENHA_VAZIO)
                                    .MinimumLength(6).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_SENHA_CURTA)
                                    .MaximumLength(200).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_SENHA_GRANDE);
        }
        public void ValidateEmail()
        {
            RuleFor(c => c.Email).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_EMAIL_VAZIO)
                                 .MinimumLength(10).WithMessage(GenericConstants.MENSGAEM_ERRO_EMAIL_CURTO)
                                 .MaximumLength(100).WithMessage(GenericConstants.MENSGAEM_ERRO_EMAIL_GRANDE)
                                 .EmailAddress().WithMessage(GenericConstants.MENSAGEM_ERRO_EMAIL_INVALIDO);
        }
        public void ValidateDataNascimento()
        {
            RuleFor(c => c.DataNascimento).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_DATANASCIMENTO_VAZIO);

        }
        public void ValidateIdade()
        {
            RuleFor(c => c.Idade).IsIdadeValid().WithMessage(GenericConstants.MENSAGEM_ERRO_IDADE_INVALIDA);

        }
        public void ValidateCPF()
        {
            RuleFor(c => c.CPF).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_CPF_VAZIO)
                               .IsCpfValid().WithMessage(GenericConstants.MENSAGEM_ERRO_CPF_INVÁLIDO);
        }
        public void ValidateRG()
        {
            RuleFor(c => c.RG).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_RG_VAZIO)
                               .WithMessage(GenericConstants.MENSAGEM_ERRO_RG_INVÁLIDO);
        }

    }
}