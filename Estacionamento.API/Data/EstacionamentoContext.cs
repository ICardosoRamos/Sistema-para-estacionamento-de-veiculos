using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Data
{
    public class EstacionamentoContext : DbContext
    {
        public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options)
        : base(options)
        { }

        public DbSet<Estacionamento> Estacionamentos { get; set; }

    }
}