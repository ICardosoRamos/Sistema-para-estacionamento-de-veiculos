using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Estacionamento> Estacionamento { get; set; }
        public DbSet<Preco> Preco { get; set; }
    }
}