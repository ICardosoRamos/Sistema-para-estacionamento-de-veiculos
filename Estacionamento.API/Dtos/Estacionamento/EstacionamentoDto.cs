using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.API.Dtos.Estacionamento
{
    public class EstacionamentoDto
    {
        public int Id { get; set; }

        public string Placa { get; set; } = string.Empty;

        public DateTime HorarioChegada { get; set; } = DateTime.Now;

        public DateTime? HorarioSaida { get; set; }

        public TimeSpan? Duracao { get; set; }

        public TimeSpan? TempoCobrado { get; set; }

        public decimal? PrecoHora { get; set; }

        public decimal? ValorAPagar { get; set; }
    }
}