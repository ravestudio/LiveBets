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
        public DbSet<Message> Messages { get; set; }
        public DbSet<updInfo> updInfo { get; set; }

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
            modelBuilder.Entity<Event>().Property(e => e.HasMessage).HasColumnName("HasMessage").IsRequired();
            modelBuilder.Entity<Event>().ToTable("EventSet");

            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<Message>().Property(m => m.Id).HasColumnName("Id");
            modelBuilder.Entity<Message>().Property(m => m.MessageBody).HasColumnName("MessageBody");
            modelBuilder.Entity<Message>().Property(m => m.Sent).HasColumnName("Sent").IsRequired();
            modelBuilder.Entity<Message>().ToTable("MessageSet");

            modelBuilder.Entity<updInfo>().HasKey(m => m.Id);
            modelBuilder.Entity<updInfo>().Property(m => m.Id).HasColumnName("Id");
            modelBuilder.Entity<updInfo>().Property(m => m.lastUpd).HasColumnName("lastUpd").IsRequired();
            modelBuilder.Entity<updInfo>().Property(m => m.updDuration).HasColumnName("updDuration").IsRequired();
            modelBuilder.Entity<updInfo>().ToTable("updInfoSet");

            base.OnModelCreating(modelBuilder);
        }
    }
}
