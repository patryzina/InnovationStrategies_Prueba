using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGT.Api.Models
{
    public class VehiculoDto
    {
        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string DniConductor { get; set; }
    }
}
