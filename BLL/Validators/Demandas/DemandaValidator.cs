using BusinessLogicalLayer.Constants;
using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators.Demandas
{
    internal class DemandaValidator : AbstractValidator<Demanda>
    {
        /// <summary>
        /// Valida o ID da Demanda.
        /// </summary>
        public void ValidateID()
        {
            RuleFor(c => c.ID).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_ID_VAZIO);
        }
        /// <summary>
        /// Valida o Nome da Demanda.
        /// </summary>
        public void ValidateNome()
        {
            RuleFor(c => c.Nome).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                          .MinimumLength(4).WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_CURTO)
                                          .MaximumLength(30).WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_GRANDE);
        }
        /// <summary>
        /// Valida a Descrição detalhada da Demanda.
        /// </summary>
        public void ValidateDescricaoDetalhada()
        {
            RuleFor(c => c.DescricaoDetalhada).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                              .MinimumLength(9).WithMessage(DemandaConstants.MENSAGEM_ERRO_DESCRICAO_DETALHADA_MENOR)
                                              .MaximumLength(100).WithMessage(DemandaConstants.MENSAGEM_ERRO_DESCRICAO_DETALHADA_MAIOR);
        }
        /// <summary>
        /// Valida a Descrição curta da Demanda.
        /// </summary>
        public void ValidateDescricaoCurta()
        {
            RuleFor(c => c.DescricaoCurta).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_NOME_VAZIO)
                                                    .MinimumLength(4).WithMessage(DemandaConstants.MENSAGEM_ERRO_DESCRICAO_CURTA_MENOR)
                                                    .MaximumLength(30).WithMessage(DemandaConstants.MENSAGEM_ERRO_DESCRICAO_CURTA_MAIOR);
        }
    }
}
