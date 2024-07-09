using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.API.Data
{
    public class Preco
    {
        
        [Key]
        public int? Id { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? PrecoPorHora { get; set; }
    }
}