using System.ComponentModel.DataAnnotations;

namespace Air2.Models
{
    public class Piloto
    {
       
        public int Id { get; set; }

        public DateTime HorasVuelo { get; set; }

        public string? Nombre { get; set; }

        public string? Base { get; set; }
    }
}
