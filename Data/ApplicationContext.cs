using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Efir.Model;
using Microsoft.EntityFrameworkCore;

namespace Efir.Data
{
    internal class ApplicationContext : DbContext, IDisposable
    {
        public DbSet<Series> Serieses { get; set; } = null!;
        public DbSet<Film> Films { get; set; } = null!;
        public DbSet<Lection> Lections { get; set; } = null!;
        public DbSet<Documentaries> Documentarieses { get; set; } = null!;
        public DbSet<Educational> Educationals { get; set; } = null!;
        public DbSet<Entertainment> Entertainments { get; set; } = null!;
        public DbSet<Prevention> Preventions { get; set; } = null!;
        public DbSet<TvShow> TvShows { get; set; } = null!;

        #region Модели событий по дням недели
        public DbSet<EfirOnMonday> OnMonday { get; set; } = null!;
        public DbSet<EfirOnTuesday> OnTuesday { get; set; } = null!;
        public DbSet<EfirOnWednesday> OnWednesday { get; set; } = null!;
        public DbSet<EfirOnThursday> OnThursday { get; set; } = null!;
        public DbSet<EfirOnFriday> OnFriday { get; set; } = null!;
        public DbSet<EfirOnSaturday> OnSaturday { get; set; } = null!;
        public DbSet<EfirOnSunday> OnSunday { get; set; } = null!;
        #endregion

        #region Модели для сбора эфира на неделю по дням неделям (для печати)
        public DbSet<PrintMonday> PrintMondays { get; set; } = null!;
        public DbSet<PrintTuesday> PrintTuesdays { get; set; } = null!;
        public DbSet<PrintWednesday> PrintWednesdays { get; set; } = null!;
        public DbSet<PrintThursday> PrintThursdays { get; set; } = null!;
        public DbSet<PrintFriday> PrintFridays { get; set; } = null!;
        public DbSet<PrintSaturday> PrintSaturdays { get; set; } = null!;
        public DbSet<PrintSunday> PrintSundays { get; set; } = null!;

        #endregion

        //public DbSet<SeriesCollection> SeriesCollections { get; set; } = null!;
        //public DbSet<DocumentariesCollection> DocumentariesCollections { get; set; } = null!;
        //public DbSet<EducationalCollection> EducationalCollections { get; set; } = null!;
        //public DbSet<EntertainmentCollection> EntertainmentCollections { get; set; } = null!;
        //public DbSet<LectionCollection> LectionCollections { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MediaDB.db");
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;

        protected virtual void Dispose(bool Disposig)
        {
            if (!Disposig || _Disposed) return;
            _Disposed = true;
            // Освобождение управляемых ресурсов
        }
    }
}
