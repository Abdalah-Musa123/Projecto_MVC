using System.ComponentModel.DataAnnotations;

namespace Air2.Models
{
    public class Tripulacion
    {
        [Key]
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }

        public string? Base { get; set; }
    }
}
