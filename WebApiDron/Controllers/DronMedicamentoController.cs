using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDron.Data;
using WebApiDron.Models;

namespace WebApiDron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DronMedicamentoController : ControllerBase
    {
        private readonly WebApiDronContext _context;

        public DronMedicamentoController(WebApiDronContext context)
        {
            _context = context;
        }

        // GET: api/DronMedicamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DronMedicamento>>> GetDronMedicamento()
        {
            return await _context.DronMedicamento.ToListAsync();
        }
        /// <summary>
        ///Se ingresa el codigo de dron para la busqueda.
        /// </summary>
        /// <returns></returns>

        // GET: api/DronMedicamento/5
        [HttpGet("{codigoDron}")]
        public async Task<ActionResult<DronMedicamento>> GetDronMedicamento(string codigoDron)
        {
            List<DronModels>  dronModels = await _context.DronModels.ToListAsync();

            var DronMedicamento = await _context.DronMedicamento.FindAsync(codigoDron);

            if (DronMedicamento == null)
            {
                return NotFound();
            }
            foreach (var item in dronModels)
            {
                if (codigoDron == item.NumeroSerie)
                {
                    //TODO:Falta implementar logica nivel bateria, peso, drones disponibles 
                }

            }

            return DronMedicamento;
        }

        // PUT: api/DronMedicamento/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDronMedicamento(int id, DronMedicamento DronMedicamento)
        {
            if (id != DronMedicamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(DronMedicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DronMedicamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DronMedicamento
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DronMedicamento>> PostDronMedicamento(DronMedicamento DronMedicamento)
        {
            _context.DronMedicamento.Add(DronMedicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDronMedicamento", new { id = DronMedicamento.Id }, DronMedicamento);
        }

        // DELETE: api/DronMedicamento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DronMedicamento>> DeleteDronMedicamento(int id)
        {
            var DronMedicamento = await _context.DronMedicamento.FindAsync(id);
            if (DronMedicamento == null)
            {
                return NotFound();
            }

            _context.DronMedicamento.Remove(DronMedicamento);
            await _context.SaveChangesAsync();

            return DronMedicamento;
        }

        private bool DronMedicamentoExists(int id)
        {
            return _context.DronMedicamento.Any(e => e.Id == id);
        }
    }
}
