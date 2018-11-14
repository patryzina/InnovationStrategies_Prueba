using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Domain.Entities
{
    public class ConductorVehiculo
    {
        public int ConductorId { get; set; }

        public Conductor Conductor { get; set; }

        public int VehiculoId { get; set; }

        public Vehiculo Vehiculo { get; set; }
    }
}
