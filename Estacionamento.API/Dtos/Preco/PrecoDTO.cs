using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.API.Dtos.Preco
{
    public class PrecoDTO
    {
        public int Id { get; set; }

        public DateTime InicioVigencia { get; set; }

        public DateTime FimVigencia { get; set; }

        public decimal ValorHora { get; set; }
    }
}