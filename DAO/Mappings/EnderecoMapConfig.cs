using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class EnderecoMapConfig : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ENDERECOS");
            builder.Property(e => e.Numero).HasMaxLength(5).IsRequired().IsUnicode(false);
            builder.Property(e => e.PontoReferencia).HasMaxLength(30).IsUnicode(false);
            builder.Property(e => e.Rua).HasMaxLength(40).IsRequired().IsUnicode(false);
            builder.Property(e => e.Complemento).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.Cidade).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(e => e.Cep).HasMaxLength(10).IsRequired().IsUnicode(false);
            builder.Property(e => e.Bairro).HasMaxLength(30).IsRequired().IsUnicode(false);
            
        }
    }
}
