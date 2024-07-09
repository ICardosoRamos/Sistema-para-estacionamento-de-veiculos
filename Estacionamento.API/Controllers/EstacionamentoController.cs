
using EstacionamentoNamespace = Estacionamento.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoController(EstacionamentoNamespace.AplicationDBContext context) : ControllerBase
    {
        private readonly EstacionamentoNamespace.AplicationDBContext _context = context;

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<EstacionamentoNamespace.Estacionamento>>> GetEstacionamentos()
        // {
        //     return await _context.Estacionamentos.ToListAsync();
        // }

        [HttpGet]
        public IActionResult GetAll()
        {
            var estacionamentos = _context.Estacionamento.ToList();

            return Ok(estacionamentos);
        }
    }
}