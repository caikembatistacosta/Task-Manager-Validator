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
            builder.Property(f => f.Nome).HasMaxLength(50).IsRequired().IsUnicode(false);
            builder.Property(f => f.Senha).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(f => f.Username).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(f => f.DataNascimento).IsRequired().HasColumnType("datetime2");
            builder.Property(f => f.Cpf).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(f => f.Rg).HasMaxLength(14).IsRequired().IsUnicode(false);
        }
    }
}
