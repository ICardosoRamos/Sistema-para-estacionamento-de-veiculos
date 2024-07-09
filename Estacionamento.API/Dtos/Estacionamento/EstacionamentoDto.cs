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

        public decimal? PrecoHora { get; set; }

        //THE REST...
    }
}