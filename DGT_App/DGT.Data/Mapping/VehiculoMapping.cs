using DGT.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DGT.Data.Infrastructure.Mapping
{
    public class VehiculoMapping : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("Vehiculo");

            builder.HasKey(v => v.Id);

            builder.Property(c => c.Matricula)
                   .HasColumnName("Matricula")
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(c => c.Marca)
                   .HasColumnName("Marca")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Modelo)
                   .HasColumnName("Modelo")
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
