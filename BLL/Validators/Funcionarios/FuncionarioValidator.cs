using BusinessLogicalLayer.Constants;
using BusinessLogicalLayer.Validators.ComonsValidators;
using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators.Funcionarios
{
    internal class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        /// <summary>
        /// Valida o ID do Funcionário.
        /// </summary>
        public void ValidateID()
        {
            RuleFor(c => c.ID).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_ID_VAZIO);
        }
        /// <summary>
        /// Valida o Nome Completo do Funcionário.
        /// </summary>
        public void ValdiateNome()
        {
            RuleFor(c => c.Nome).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                               .MinimumLength(3).WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_CURTO)
                               .MaximumLength(30).WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_GRANDE);

            RuleFor(c => c.Sobrenome).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                     .MinimumLength(3).WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_CURTO)
                                     .MaximumLength(30).WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_GRANDE);
        }
        /// <summary>
        /// Valida a Senha do Funcionário.
        /// </summary>
        public void ValidateSenha()
        {
            RuleFor(c => c.Senha).NotNull().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_SENHA_VAZIO)
                                 .MinimumLength(6).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_SENHA_CURTA)
                                 .MaximumLength(20).WithMessage(FuncionariosConstants.MENSAGEM_ERRO_SENHA_GRANDE);
        }
        /// <summary>
        /// Valida o Email do Funcionário.
        /// </summary>
        public void ValidateEmail()
        {
            RuleFor(c => c.Email).NotNull().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_EMAIL_VAZIO)
                             .MinimumLength(10).WithMessage(FuncionariosConstants.MENSGAEM_ERRO_EMAIL_CURTO)
                             .MaximumLength(100).WithMessage(FuncionariosConstants.MENSGAEM_ERRO_EMAIL_GRANDE)
                             .EmailAddress().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_EMAIL_INVALIDO);
        }
        /// <summary>
        /// Valida a Data de Nascimento do Funcionário.
        /// </summary>
        public void ValidateDataNascimento()
        {
            RuleFor(c => c.DataNascimento).NotNull().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_DATA_VAZIO);
        }
        /// <summary>
        /// Valida o CPF do Funcionário.
        /// </summary>
        public void ValidateCPF()
        {
            RuleFor(c => c.CPF).NotNull().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_CPF_VAZIO)
                               .IsCpfValid().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_CPF_INVALIDO);
        }
        /// <summary>
        /// Valida o RG do Funcionário.
        /// </summary>
        public void ValidateRG()
        {
            RuleFor(c => c.RG).NotNull().WithMessage(FuncionariosConstants.MENSAGEM_ERRO_RG_VAZIO)
                              .Matches(@"^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$").WithMessage(FuncionariosConstants.MENSAGEM_ERRO_RG_INVALIDO);
        }
    }
}
