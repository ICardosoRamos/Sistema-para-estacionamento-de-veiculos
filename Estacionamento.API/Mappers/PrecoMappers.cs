using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.API.Dtos.Preco;
using Estacionamento.API.Models;

namespace Estacionamento.API.Mappers
{
    public static class PrecoMappers
    {
        public static PrecoDTO ToPrecoDTO(this Preco precoModel)
        {
            return new PrecoDTO
            {
                Id = precoModel.Id,
                InicioVigencia = precoModel.InicioVigencia,
                FimVigencia = precoModel.FimVigencia,
                ValorHora = precoModel.ValorHora,
            };
        }

        public static Preco ToPrecoFromCreateDTO(this CreatePrecoRequestDTO precoDTO)
        {
            return new Preco
            {
                InicioVigencia = precoDTO.InicioVigencia,
                FimVigencia = precoDTO.FimVigencia,
                ValorHora = precoDTO.ValorHora,
            };
        }

        public static Preco ToPrecoFromUpdateDTO(this UpdatePrecoRequestDTO precoDTO)
        {
            return new Preco
            {
                InicioVigencia = precoDTO.InicioVigencia,
                FimVigencia = precoDTO.FimVigencia,
                ValorHora = precoDTO.ValorHora,
            };
        }

    }
}