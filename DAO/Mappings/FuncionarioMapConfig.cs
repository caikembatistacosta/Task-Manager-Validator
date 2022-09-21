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
    internal class FuncionarioMapConfig : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("FUNCIONARIOS");
            builder.Property(f => f.Email).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Property(f => f.Nome).HasMaxLength(30).IsRequired().IsUnicode(false);
            builder.Property(f => f.Sobrenome).HasMaxLength(30).IsRequired().IsUnicode(false);
            builder.Property(f => f.Senha).HasMaxLength(200).IsRequired().IsUnicode(false);
            builder.Property(f => f.DataNascimento).IsRequired().HasColumnType("datetime2");
            builder.Property(f => f.CPF).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.HasIndex(f => f.CPF).IsUnique();
            builder.Property(f => f.RG).HasMaxLength(14).IsRequired().IsUnicode(false);
            builder.HasIndex(f => f.RG).IsUnique();
            builder.Property(f => f.Genero).IsRequired().IsUnicode(false);
            builder.Property(f => f.NivelDeAcesso).IsRequired().IsUnicode(false);
            builder.Property(f => f.IsAtivo).IsRequired().IsUnicode(false);

        }
    }
}