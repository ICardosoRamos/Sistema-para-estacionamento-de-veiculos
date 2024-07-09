
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.API.Models
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