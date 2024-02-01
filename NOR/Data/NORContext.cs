using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NOR.Models;

namespace NOR.Data
{
    public class NORContext : DbContext
    {
        public NORContext (DbContextOptions<NORContext> options)
            : base(options)
        {
        }

        public DbSet<NOR.Models.RegistoUtilizador> RegistoUtilizador { get; set; } = default!;
    }
}
