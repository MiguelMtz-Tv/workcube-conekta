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

            modelBuilder.Entity<AspNetUser>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario)
                .HasForeignKey<AspNetUser>(u => u.IdCliente);
                
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
