// using Estacionamento.API.Models;
using EstacionamentoNamespace = Estacionamento.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<EstacionamentoNamespace.Estacionamento> Estacionamento { get; set; }
        public DbSet<EstacionamentoNamespace.Preco> Preco { get; set; }
    }
}