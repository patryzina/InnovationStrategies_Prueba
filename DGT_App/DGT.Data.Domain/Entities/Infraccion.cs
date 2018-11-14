using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DGT.Data.Domain.Entities
{
    public class Infraccion
    {
        public Guid Id { get; set; }

        [ForeignKey("Conductor")]
        public int ConductorId { get; set; }

        [ForeignKey("TipoInfraccion")]
        public string IdTipoInfraccion { get; set; }

        public DateTime FechaInfraccion{ get; set; }

        // Campos para Navegación
        public TipoInfraccion TipoInfraccion { get; set; }
        public Conductor Conductor { get; set; }
    }
}
