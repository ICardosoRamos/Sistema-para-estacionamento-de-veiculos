using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.API.Dtos.Estacionamento
{
    public class CreateEstacionamentoRequestDto
    {

        public string Placa { get; set; } = string.Empty;
    }
}