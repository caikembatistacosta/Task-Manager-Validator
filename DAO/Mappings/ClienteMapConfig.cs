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
    internal class ClienteMapConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTES");
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(p => p.CPF).IsFixedLength().HasMaxLength(11).IsUnicode(false);
            builder.Property(p => p.DataNascimento).IsRequired().HasColumnType("datetime2");
            builder.HasOne(c => c.Endereco).WithOne(c => c.Cliente).HasConstraintName("FK_ENDERECO_CLIENTE");
        }
    }
}
