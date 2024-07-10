using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.API.Data;
using Estacionamento.API.Dtos.Preco;
using Estacionamento.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrecoController(AplicationDBContext context) : ControllerBase
    {
        private readonly AplicationDBContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var precos = _context.Precos.ToList().Select(preco => preco.ToPrecoDTO());

            return Ok(precos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var preco = _context.Precos.Find(id);

            if (preco == null)
            {
                return NotFound();
            }

            return Ok(preco.ToPrecoDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePrecoRequestDTO precoDTO)
        {
            var precoModel = precoDTO.ToPrecoFromCreateDTO();

            var precoExistenteNaVigencia = _context.Precos.Where(
                p => (precoDTO.InicioVigencia >= p.InicioVigencia && precoDTO.InicioVigencia <= p.FimVigencia) ||
                    (precoDTO.FimVigencia >= p.InicioVigencia && precoDTO.FimVigencia <= p.FimVigencia) ||
                    (precoDTO.InicioVigencia <= p.InicioVigencia && precoDTO.FimVigencia >= p.FimVigencia)
            ).FirstOrDefault();

            if (precoExistenteNaVigencia != null)
            {
                return BadRequest(
                    "Já existe um preço no período entre " +
                    precoExistenteNaVigencia.InicioVigencia +
                    " e " +
                    precoExistenteNaVigencia.FimVigencia +
                    " então não é possível adicionar com o período atual selecionado!"
                );
            }

            _context.Precos.Add(precoModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = precoModel.Id, }, precoModel.ToPrecoDTO());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var preco = _context.Precos.FirstOrDefault(x => x.Id == id);



            if (preco == null)
            {
                return NotFound();
            }


            _context.Precos.Remove(preco);

            _context.SaveChanges();



            return NoContent();
        }

    }
}