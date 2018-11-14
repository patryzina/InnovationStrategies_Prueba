using DGT.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Infrastructure.Mapping
{
    public class ConductorVehiculoMapping : IEntityTypeConfiguration<ConductorVehiculo>
    {
        public void Configure(EntityTypeBuilder<ConductorVehiculo> builder)
        {
            builder.ToTable("ConductorVehiculo");

            builder.HasKey(c => new { c.ConductorId, c.VehiculoId });
        }
    }
}
