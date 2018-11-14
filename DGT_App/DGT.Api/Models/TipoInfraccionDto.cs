using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGT.Api.Models
{
    // Nota: A mi me gusta particularmente crear entidades separadas para clases de dominio de los modelos
    // que exponemos en nuestra API, aunque estas sean similares, así hacemos mas facil una posible refactorización
    // en el caso de que queramos añadir alguna lógica y aplicamos el principio de "Separation of Concerns"
    public class TipoInfraccionDto
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public int CostePuntos { get; set; }
    }
}
