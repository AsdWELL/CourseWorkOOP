using Microsoft.EntityFrameworkCore;

namespace CourseWork
{
    /// <summary>
    /// Контекст данных
    /// </summary>
    public class MuseumContext : DbContext
    {
        /// <summary>
        /// Таблица посетителей
        /// </summary>
        public DbSet<Visitor> Visitors { get; set; } = null!;
        /// <summary>
        /// Таблица экспонатов
        /// </summary>
        public DbSet<Exhibit> Exhibits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=museum.db");
        }
    }
}
