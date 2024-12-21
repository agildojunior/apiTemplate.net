using Microsoft.EntityFrameworkCore;
using DigitalShelf.Domain.Models;

namespace DigitalShelf.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //--- Usuario ---
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); // Define o Id como autoincrementado

            modelBuilder.Entity<Usuario>()
                .ToTable("tp_usuario"); // Mapeia para a tabela existente no banco

        }
    }
}
