using DGT.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Infrastructure.Mapping
{
    public class InfraccionMapping : IEntityTypeConfiguration<Infraccion>
    {
        public void Configure(EntityTypeBuilder<Infraccion> builder)
        {
            builder.ToTable("Infraccion");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FechaInfraccion)
                   .HasColumnName("FechaInfraccion")
                   .IsRequired();
        }
    }
}
