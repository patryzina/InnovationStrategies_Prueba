using DGT.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Infrastructure.Repositories
{
    public interface IDgtRepository
    {
        void AddConductor(Conductor vehiculo);

        void AddVehiculo(Vehiculo vehiculo);

        void AddTipoInfraccion(TipoInfraccion vehiculo);

        void AddInfraccion(Infraccion vehiculo);

        IEnumerable<Infraccion> GetInfraccionesConductor(string dni);

        IEnumerable<TipoInfraccion> GetInfraccionesComunes(int topMax);

        IEnumerable<Conductor> GeTopNConductores(int topMax);
    }
}
