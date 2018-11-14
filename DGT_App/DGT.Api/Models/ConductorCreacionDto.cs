namespace DGT.Api.Models
{
    // Nota: A mi me gusta particularmente crear entidades separadas para clases de dominio de los modelos
    // que exponemos en nuestra API, aunque estas sean similares, así hacemos mas facil una posible refactorización
    // en el caso de que queramos añadir alguna lógica y aplicamos el principio de "Separation of Concerns"
    public class ConductorCreacionDto
    {
        public string DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public int Puntos { get; set; }
    }
}
