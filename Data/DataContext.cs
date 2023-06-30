global using Microsoft.EntityFrameworkCore;

namespace workcube_pagos.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relacion usuario/cliente 1:1
            modelBuilder.Entity<AspNetUser>().HasOne(u => u.Cliente).WithOne(c => c.Usuario).HasForeignKey<AspNetUser>(u => u.IdCliente);
            modelBuilder.Entity<Cliente>().HasIndex(c => c.NumeroContrato).IsUnique();

            //AspAdmin relations
            //relacion Cliente/AspAdmin N:1
            modelBuilder.Entity<Cliente>().HasOne(c => c.AspAdmin).WithMany(a => a.Clientes).HasForeignKey(c => c.IdCreatedUser).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cliente>().HasOne(c => c.AspAdmin).WithMany(a => a.Clientes).HasForeignKey(c => c.IdUpdatedUser).OnDelete(DeleteBehavior.Restrict);
            //relacion Cupon/AspAdmin N:1
            modelBuilder.Entity<Cupon>().HasOne(c => c.AspAdmin).WithMany(a => a.Cupones).HasForeignKey(c => c.IdCreatedUser).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cupon>().HasOne(c => c.AspAdmin).WithMany(a => a.Cupones).HasForeignKey(c => c.IdUpdatedUser).OnDelete(DeleteBehavior.Restrict);
            //relcion Servicio/AspAdmin N:1
            modelBuilder.Entity<Servicio>().HasOne(s => s.AspAdmin).WithMany(a => a.Servicios).HasForeignKey(s => s.IdCreatedUser).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Servicio>().HasOne(s => s.AspAdmin).WithMany(a => a.Servicios).HasForeignKey(s => s.IdUpdatedUser).OnDelete(DeleteBehavior.Restrict);


            //Relacion servicioTipo/servicio 1:N
            modelBuilder.Entity<Servicio>().HasOne(s => s.ServicioTipo).WithMany(st => st.Servicio).HasForeignKey(s => s.IdServicioTipo);
            //relacion servicio/periodo N:1
            modelBuilder.Entity<Servicio>().HasOne(s => s.Periodo).WithMany(p => p.Servicio).HasForeignKey(s => s.IdPeriodo);
            
            //relacion cliente/servicio 1:N
            modelBuilder.Entity<Servicio>().HasOne(s => s.Cliente).WithMany(c => c.Servicios).HasForeignKey(s => s.IdCliente);


            //relacion cupon/servicio N:1
            modelBuilder.Entity<Cupon>().HasOne(c => c.Servicio).WithMany(s => s.Cupones).HasForeignKey(c => c.IdServicio).OnDelete(DeleteBehavior.Restrict);
            //relacion cupon/cliente N:1
            modelBuilder.Entity<Cupon>().HasOne(c => c.Cliente).WithMany(c => c.Cupones).HasForeignKey(c => c.IdCliente).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Pago>().HasIndex(s => s.Folio).IsUnique();
            //relacion pagos/clientes N:1
            modelBuilder.Entity<Pago>().HasOne(p => p.Cliente).WithMany(c => c.Pagos).HasForeignKey(p => p.IdCliente).OnDelete(DeleteBehavior.Restrict); ;
            //relacion pagos/servicios N:1 
            modelBuilder.Entity<Pago>().HasOne(p => p.Servicio).WithMany(s => s.Pagos).HasForeignKey(p => p.IdServicio).OnDelete(DeleteBehavior.Restrict);


        }

        public DbSet<AspAdmin> AspAdmins                { get; set; }
        public DbSet<AspNetUser> AspNetUsers            { get; set; }
        public DbSet<Cliente> Clientes                  { get; set; }
        public DbSet<ServicioTipo> ServiciosTipos       { get; set; }
        public DbSet<Servicio> Servicios                { get; set; }
        public DbSet<Cupon> Cupones                     { get; set; }
        public DbSet<Periodo> Periodos                  { get; set; }
        public DbSet<Pago> Pagos                        { get; set; } 
    }
}
