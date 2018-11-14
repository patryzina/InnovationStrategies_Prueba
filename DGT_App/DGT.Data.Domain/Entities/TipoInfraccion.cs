using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Domain.Entities
{
    public class TipoInfraccion
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public int CostePuntos { get; set; }
    }
}
