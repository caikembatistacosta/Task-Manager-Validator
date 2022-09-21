using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using BLL.Validators.ComonsValidators;
using BLL.Constants;

namespace BLL.Validators.Clientes
{
    internal class ClienteValidator : AbstractValidator<Cliente>
    {
        public void ValidateID()
        {
            RuleFor(c => c.ID).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_ID_VAZIO);
        }
        public void ValidateNome()
        {
            RuleFor(c => c.Nome).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                .MinimumLength(3).WithMessage(ClienteConstants.MENSAGEM_ERRO_NOME_CURTO)
                                .MaximumLength(30).WithMessage(ClienteConstants.MENSAGEM_ERRO_NOME_GRANDE);
        }
        public void ValidateCPF()
        {
            RuleFor(c => c.CPF).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_CPF_VAZIO)
                               .IsCpfValid().WithMessage(GenericConstants.MENSAGEM_ERRO_CPF_INVÁLIDO);
        }

        public void ValidateEmail()
        {
            RuleFor(c => c.Email).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_EMAIL_VAZIO)
                                 .MinimumLength(10).WithMessage(GenericConstants.MENSGAEM_ERRO_EMAIL_CURTO)
                                 .MaximumLength(100).WithMessage(GenericConstants.MENSGAEM_ERRO_EMAIL_GRANDE)
                                 .EmailAddress().WithMessage(GenericConstants.MENSAGEM_ERRO_EMAIL_INVALIDO);
        }
    }
}
