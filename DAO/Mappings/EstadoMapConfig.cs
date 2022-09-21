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
    internal class EstadoMapConfig : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("ESTADOS");
            builder.Property(e => e.NomeEstado).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(e => e.UF).HasMaxLength(2).IsRequired().IsUnicode(false);
        }
    }
}
