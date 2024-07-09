

using Estacionamento.API.Data;
using Estacionamento.API.Dtos.Estacionamento;
using Estacionamento.API.Mappers;
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
            var estacionamentos = _context.Estacionamento.ToList().Select(estacionamento => estacionamento.ToEstacionamentoDto());

            return Ok(estacionamentos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var estacionamento = _context.Estacionamento.Find(id);

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
            estacionamentoModel.PrecoHora = 2m;
            _context.Estacionamento.Add(estacionamentoModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = estacionamentoModel.Id, }, estacionamentoModel.ToEstacionamentoDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id)
        {
            var estacionamento = _context.Estacionamento.Find(id);


            if (estacionamento == null)
            {
                return NotFound();
            }

            estacionamento.HorarioSaida = DateTime.Now;
            estacionamento.Duracao = estacionamento.HorarioSaida - estacionamento.HorarioChegada;

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
            var estacionamento = _context.Estacionamento.FirstOrDefault(x => x.Id == id);



            if (estacionamento == null)
            {
                return NotFound();
            }


            _context.Estacionamento.Remove(estacionamento);

            _context.SaveChanges();



            return NoContent();
        }
    }
}