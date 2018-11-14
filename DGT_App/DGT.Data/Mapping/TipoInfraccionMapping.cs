using DGT.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Infrastructure.Mapping
{
    public class TipoInfraccionMapping : IEntityTypeConfiguration<TipoInfraccion>
    {
        public void Configure(EntityTypeBuilder<TipoInfraccion> builder)
        {
            builder.ToTable("TipoInfraccion");

            builder.HasKey(v => v.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("Id")
                   .IsRequired()
                   .HasMaxLength(25);

            builder.Property(c => c.Descripcion)
                   .HasColumnName("Descripcion")
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(c => c.CostePuntos)
                   .HasColumnName("CostePuntos")
                   .IsRequired();
        }
    }
}
