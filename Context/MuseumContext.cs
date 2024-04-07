﻿using Microsoft.EntityFrameworkCore;

namespace CourseWork
{
    public class MuseumContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; } = null!;
        public DbSet<Exhibit> Exhibits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=museum.db");
        }
    }
}
