using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Efir.Model;
using Microsoft.EntityFrameworkCore;

namespace Efir.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Series> Serieses { get; set; } = null!;
        public DbSet<Film> Films { get; set; } = null!;
        public DbSet<Lection> Lections { get; set; } = null!;
        public DbSet<Documentaries> Documentarieses { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MediaDB.db");
        }
    }
}
