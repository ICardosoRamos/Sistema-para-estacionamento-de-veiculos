
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.API.Models
{
    public class Estacionamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Placa is required")]
        public string Placa { get; set; } = string.Empty;

        public DateTime HorarioChegada { get; set; } = DateTime.Now;

        public DateTime? HorarioSaida { get; set; }

        public TimeSpan? Duracao { get; set; }

        public TimeSpan? TempoCobrado { get; set; }

        public decimal? PrecoHora { get; set; }

        public decimal? ValorAPagar { get; set; }
    }
}