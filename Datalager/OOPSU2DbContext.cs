using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiteter;
using Microsoft.EntityFrameworkCore;

namespace Datalager
{
    public class OOPSU2DbContext : DbContext
    {
        public DbSet<Medlem> Medlemmar { get; set; } //representerar tabellerna i databasen.
        public DbSet<Bokning> Bokningar { get; set; }
        public DbSet<Personal> Personaler { get; set; }
        public DbSet<Resurs> Resurser { get; set; }
        public DbSet<Utrustning> Utrustningar { get; set; }
        public DbSet<Betalning> Betalningar { get; set; }

        public OOPSU2DbContext() { } // Parameterlös konstruktor som används vid initiering i UnitOfWork.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //anslutningen till sql-servern.
        {
            optionsBuilder.UseSqlServer(
                "Server=sqlutb4-db.hb.se,56077;" +
                "Database=oosu2607;" +
                "User Id=oosu2607;" +
                "Password=JGK356;" +
                "TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Förhindrar dubbelbokning av samma resurs/utrustning.
        {
            modelBuilder.Entity<Bokning>()
                .HasIndex(b => new { b.ResursID, b.Datum, b.Starttid, b.Sluttid })
                .IsUnique();

        }
    }
}
