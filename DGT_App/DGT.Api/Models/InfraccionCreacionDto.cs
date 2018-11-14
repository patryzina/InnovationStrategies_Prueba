using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGT.Api.Models
{
    public class InfraccionCreacionDto
    {
        public string MatriculaVehiculo { get; set; }

        public string IdTipoInfraccion { get; set; }

        public DateTime FechaInfraccion { get; set; }
    }
}
