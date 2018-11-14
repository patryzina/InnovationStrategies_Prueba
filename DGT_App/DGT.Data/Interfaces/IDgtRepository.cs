using DGT.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Infrastructure.Interfaces
{
    public interface IDgtRepository
    {
        void AddConductor(Conductor conductor);

        void AddVehiculo(Vehiculo vehiculo, string dniConductor);

        void AddTipoInfraccion(TipoInfraccion tipoInfraccion);

        void AddInfraccion(Infraccion infraccion);

        Conductor GetConductor(string dni);

        Vehiculo GetVehiculo(string matricula);

        TipoInfraccion GetTipoInfraccion(string tipoInfraccion);

        List<Vehiculo> GetVehiculosParaConductor(string dniConductor);

        List<Infraccion> GetInfraccionesConductor(string dniConductor);

        List<TipoInfraccion> GetTiposInfraccionesComunes(int num);

        List<Conductor> GetMejoresConductores(int topN);

        void UpdateConductor(Conductor conductor);

        bool Save();
    }
}
