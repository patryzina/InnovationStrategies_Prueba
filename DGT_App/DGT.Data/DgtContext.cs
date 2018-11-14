using DGT.Data.Domain.Entities;
using DGT.Data.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DGT.Data.Infrastructure
{
    public class DgtContext : DbContext
    {
        public DgtContext(DbContextOptions<DgtContext> options) : base(options)
        {
            Database.Migrate();
        }

        #region Tables

        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<Conductor> Conductores { get; set; }
        public virtual DbSet<Infraccion> Infracciones { get; set; }
        public virtual DbSet<TipoInfraccion> TiposDeInfracciones { get; set; }
        public virtual DbSet<ConductorVehiculo> ConductoresVehiculos { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehiculoMapping());
            modelBuilder.ApplyConfiguration(new ConductorMapping());
            modelBuilder.ApplyConfiguration(new TipoInfraccionMapping());
            modelBuilder.ApplyConfiguration(new InfraccionMapping());
            modelBuilder.ApplyConfiguration(new ConductorVehiculoMapping());
        }
    }
}
