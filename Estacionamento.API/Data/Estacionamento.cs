
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.API.Data
{
    public class Estacionamento
    {
        [Key]
        public int Id { get; set; }

        public string Placa { get; set; } = string.Empty;

        public DateTime HorarioChegada { get; set; }

        public DateTime HorarioSaida { get; set; }

        public TimeSpan Duracao { get; set; }

        public TimeSpan TempoCobrado { get; set; }

        public decimal Preco { get; set; }

        public decimal ValorAPagar { get; set; }



        public Estacionamento()
        {
            Placa = string.Empty;
            Duracao = TimeSpan.Zero;
            TempoCobrado = TimeSpan.Zero;
            Preco = 0m;
            ValorAPagar = 0m;
        }
    }
}