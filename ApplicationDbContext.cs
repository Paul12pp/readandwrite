using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LeeryEscribir
{
    public partial class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext()
            : base("name=DbContext")
        {
        }

        public virtual DbSet<Empleados> Empleados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Sueldo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Moneda)
                .IsUnicode(false);
        }
    }
}
