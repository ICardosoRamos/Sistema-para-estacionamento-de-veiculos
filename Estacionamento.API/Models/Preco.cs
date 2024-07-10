
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.API.Models
{
    public class Preco
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime InicioVigencia { get; set; }

        [Required]
        public DateTime FimVigencia { get; set; }

        [Required]
        public decimal ValorHora { get; set; }
    }
}