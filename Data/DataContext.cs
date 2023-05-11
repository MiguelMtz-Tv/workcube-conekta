global using Microsoft.EntityFrameworkCore;

namespace workcube_pagos.Data
{
    public class DataContext : IdentityDbContext //DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioModel>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario)
                .HasForeignKey<UsuarioModel>(u => u.IdCliente);
                
        }

        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
    }
}
