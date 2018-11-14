using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Domain.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        // Relación Many-to-Many . EF creara automatiamente la tabla para el join
        public List<ConductorVehiculo> ConductoresHabituales { get; set; } = new List<ConductorVehiculo>();
    }
}
