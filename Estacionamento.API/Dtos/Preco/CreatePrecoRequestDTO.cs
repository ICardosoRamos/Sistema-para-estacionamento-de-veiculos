using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.API.Dtos.Preco
{
    public class CreatePrecoRequestDTO
    {
        public DateTime InicioVigencia { get; set; }

        public DateTime FimVigencia { get; set; }

        public decimal ValorHora { get; set; }
    }
}