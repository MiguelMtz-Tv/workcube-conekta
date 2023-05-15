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

            //relacion cliente/servicio 1:N
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Cliente)
                .WithMany(c => c.Servicios)
                .HasForeignKey(s => s.IdCliente);

            //relacion cupon/servicio N:1
            modelBuilder.Entity<Cupon>()
                .HasOne(c => c.Servicio)
                .WithMany(s => s.Cupones)
                .HasForeignKey(c => c.IdServicio).OnDelete(DeleteBehavior.NoAction);


            //relacion cupon/cliente N:1
            modelBuilder.Entity<Cupon>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Cupones)
                .HasForeignKey(c => c.IdCliente).OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ServicioTipo> ServicioTipos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Cupon> Cupones { get; set; }
    }
}
