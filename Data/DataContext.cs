global using Microsoft.EntityFrameworkCore;
using workcube_pagos.Models;

namespace workcube_pagos.Data
{
    public class DataContext : IdentityDbContext //DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relacion usuario/cliente 1:1
            modelBuilder.Entity<AspNetUser>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario)
                .HasForeignKey<AspNetUser>(u => u.IdCliente);

            //Relacion servicioTipo/servicio 1:N
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.ServicioTipo)
                .WithMany(st => st.Servicio)
                .HasForeignKey(s => s.IdServicioTipo);

            //relacion servicio/periodo N:1
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Periodo)
                .WithMany(p => p.Servicio)
                .HasForeignKey(s => s.IdPeriodo);

            //relacion servicio/status
            modelBuilder.Entity<Servicio>().HasOne(s => s.ServicioEstatus).WithMany(p => p.Servicios).HasForeignKey(s => s.IdServicioEstatus).OnDelete(DeleteBehavior.Restrict);

            //relacion cliente/servicio 1:N
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Cliente)
                .WithMany(c => c.Servicios)
                .HasForeignKey(s => s.IdCliente);

            //relacion cupon/servicio N:1
            modelBuilder.Entity<Cupon>()
                .HasOne(c => c.Servicio)
                .WithMany(s => s.Cupones)
                .HasForeignKey(c => c.IdServicio).OnDelete(DeleteBehavior.Restrict);


            //relacion cupon/cliente N:1
            modelBuilder.Entity<Cupon>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Cupones)
                .HasForeignKey(c => c.IdCliente).OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ServicioTipo> ServiciosTipos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Cupon> Cupones { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<ServicioEstatus> ServiciosEstatus { get; set; }
    }
}
