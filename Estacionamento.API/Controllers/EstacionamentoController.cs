

using Estacionamento.API.Data;
using Estacionamento.API.Dtos.Estacionamento;
using Estacionamento.API.Mappers;
using Estacionamento.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoController(AplicationDBContext context) : ControllerBase
    {
        private readonly AplicationDBContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var estacionamentos = _context.Estacionamentos.ToList().Select(estacionamento => estacionamento.ToEstacionamentoDto());

            return Ok(estacionamentos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var estacionamento = _context.Estacionamentos.Find(id);

            if (estacionamento == null)
            {
                return NotFound();
            }

            return Ok(estacionamento.ToEstacionamentoDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEstacionamentoRequestDto estacionamentoDto)
        {
            
            var estacionamentoModel = estacionamentoDto.ToEstacionamentoFromCreateDTO();

            var existePlaca = _context.Estacionamentos.Where(e => e.Placa == estacionamentoModel.Placa).FirstOrDefault();

            if (existePlaca != null) {
                return BadRequest("Já existe um cadastro com este placa no sistema!");
            }

            var preco = _context.Precos.Where(p => estacionamentoModel.HorarioChegada >= p.InicioVigencia && estacionamentoModel.HorarioChegada <= p.FimVigencia).FirstOrDefault();

            if (preco == null)
            {
                return BadRequest("Não existe um preço cadastrado para o período selecionado, primeiro cadastre o preço e depois o veículo!");
            }

            estacionamentoModel.PrecoHora = preco.ValorHora;

            _context.Estacionamentos.Add(estacionamentoModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = estacionamentoModel.Id, }, estacionamentoModel.ToEstacionamentoDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id)
        {
            var estacionamento = _context.Estacionamentos.Find(id);


            if (estacionamento == null)
            {
                return NotFound();
            }

            if (estacionamento.HorarioChegada > DateTime.Now)
            {
                return BadRequest("Você não pode marcar uma saída para esse veículo pois ele foi cadastrado para entrar na garagem no futuro!");
            }

            estacionamento.HorarioSaida = DateTime.Now;
            estacionamento.Duracao = estacionamento.HorarioSaida - estacionamento.HorarioChegada;

            // var precoSelecionado = _context.Precos.Where(x => x.InicioVigencia <= estacionamento.HorarioChegada && x.FimVigencia >= estacionamento.HorarioChegada);

            // estacionamento.PrecoHora = 2m;



            var duracaoTotal = estacionamento.Duracao.Value.TotalMinutes;

            if (duracaoTotal <= 30)
            {
                estacionamento.TempoCobrado = TimeSpan.FromMinutes(30);
                estacionamento.ValorAPagar = estacionamento.PrecoHora / 2;
            }
            else
            {
                var horasCompletas = Math.Floor(duracaoTotal / 60);
                var minutosRestantes = duracaoTotal % 60;

                if (minutosRestantes <= 10)
                {
                    estacionamento.TempoCobrado = TimeSpan.FromHours(horasCompletas);
                    estacionamento.ValorAPagar = (decimal)horasCompletas * estacionamento.PrecoHora;
                }
                else
                {
                    estacionamento.TempoCobrado = TimeSpan.FromHours(horasCompletas + 1);
                    estacionamento.ValorAPagar = (decimal)(horasCompletas + 1) * estacionamento.PrecoHora;
                }
            }

            _context.Entry(estacionamento).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var estacionamento = _context.Estacionamentos.FirstOrDefault(x => x.Id == id);



            if (estacionamento == null)
            {
                return NotFound();
            }


            _context.Estacionamentos.Remove(estacionamento);

            _context.SaveChanges();



            return NoContent();
        }
    }
}