
using EstacionamentoNamespace = Estacionamento.API.Models;

using Estacionamento.API.Dtos.Estacionamento;

namespace Estacionamento.API.Mappers
{
    public static class EstacionamentoMappers
    {
        public static EstacionamentoDto ToEstacionamentoDto(this EstacionamentoNamespace.Estacionamento estacionamentoModel)
        {
            return new EstacionamentoDto
            {
                Id = estacionamentoModel.Id,
                Placa = estacionamentoModel.Placa,
            };
        }

        public static EstacionamentoNamespace.Estacionamento ToEstacionamentoFromCreateDTO(this CreateEstacionamentoRequestDto estacionamentoDto)
        {
            return new EstacionamentoNamespace.Estacionamento
            {
                Placa = estacionamentoDto.Placa,
            };
        }
    }
}