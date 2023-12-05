using System.ComponentModel.DataAnnotations;

namespace Air2.Models
{
    public class Vuelo
    {
        [Key]
        public string? Codigo { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public DateTime HoraVuelo { get; set; }

        public string? BaseMantenimiento { get; set; }
    }
}
