using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Air2.Models;

namespace Air2.Data
{
    public class Air2Context : DbContext
    {
        public Air2Context (DbContextOptions<Air2Context> options)
            : base(options)
        {
        }

        public DbSet<Air2.Models.Piloto> Piloto { get; set; } = default!;

        public DbSet<Air2.Models.Tripulacion> Tripulacion { get; set; } = default!;

        public DbSet<Air2.Models.Vuelo> Vuelo { get; set; } = default!;
    }
}
