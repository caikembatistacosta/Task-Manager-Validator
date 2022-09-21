using BLL.Constants;
using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Demandas
{
    internal class DemandaValidator : AbstractValidator<Demanda>
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

        public void ValidateDescricaoDetalhada()
        {
            RuleFor(c => c.DescricaoCurta).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
        .MinimumLength(3).WithMessage(ClienteConstants.MENSAGEM_ERRO_NOME_CURTO)
        .MaximumLength(30).WithMessage(ClienteConstants.MENSAGEM_ERRO_NOME_GRANDE);
        }
        public void ValidateDescricaoCurta()
        {
            RuleFor(c => c.DescricaoDetalhada).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
.MinimumLength(10).WithMessage(ClienteConstants.MENSAGEM_ERRO_NOME_CURTO)
.MaximumLength(100).WithMessage(ClienteConstants.MENSAGEM_ERRO_NOME_GRANDE);
        }
    }
}
