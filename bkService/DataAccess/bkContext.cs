using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace bkService.DataAccess
{
    public class bkContext: DbContext
    {
        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bkService.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Event>().HasKey(e => e.Id);
            modelBuilder.Entity<Event>().Property(e => e.Id).HasColumnName("Id");

            modelBuilder.Entity<Event>().Property(e => e.EventId).HasColumnName("EventId");
            modelBuilder.Entity<Event>().Property(e => e.jsonData).HasColumnName("data").IsRequired();
            modelBuilder.Entity<Event>().ToTable("EventSet");

            base.OnModelCreating(modelBuilder);
        }
    }
}
