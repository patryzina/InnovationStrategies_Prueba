using DGT.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Infrastructure.Mapping
{
    public class ConductorMapping : IEntityTypeConfiguration<Conductor>
    {
        public void Configure(EntityTypeBuilder<Conductor> builder)
        {
            builder.ToTable("Conductor");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DNI)
                   .HasColumnName("DNI")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Nombre)
                   .HasColumnName("Nombre")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Apellidos)
                   .HasColumnName("Apellidos")
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.Puntos)
                   .HasColumnName("Puntos")
                   .IsRequired();
        }
    }
}
