using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficesAPI.Models;

namespace OfficesAPI.Data
{
    public class OfficesAPIContext : DbContext
    {
        public OfficesAPIContext (DbContextOptions<OfficesAPIContext> options)
            : base(options)
        {
        }

        public DbSet<OfficesAPI.Models.Berles> Berles { get; set; } = default!;
    }
}
