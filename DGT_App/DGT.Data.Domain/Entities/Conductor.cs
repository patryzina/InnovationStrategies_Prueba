using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Domain.Entities
{
    public class Conductor
    {
        public int Id { get; set; }

        public string DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public int Puntos { get; set; }

        // Relación Many-to-Many . EF creara automatiamente la tabla para el join
        public List<ConductorVehiculo> VehiculosHabituales { get; set; } = new List<ConductorVehiculo>();
    }
}
