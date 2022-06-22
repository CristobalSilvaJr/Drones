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
            List<DronModels> dronModels = await _context.DronModels.ToListAsync();
            List<MedicamentoModels> medicamentoModels = await _context.MedicamentoModels.ToListAsync();
            List<DronMedicamento> dronMedicamentos = await _context.DronMedicamento.ToListAsync();

            var DronMedicamento = await _context.DronMedicamento.FindAsync(codigoDron);
            if (DronMedicamento == null)
            {
                return NotFound();
            }
            foreach (var dronMedicamento in dronMedicamentos)
            {
                if (dronMedicamento.CodigoDron == codigoDron)
                {
                    foreach (var medicamento in medicamentoModels.Where(c => c.Codigo == dronMedicamento.CodigoMedicamento))
                    {
                        if (medicamento.Codigo == dronMedicamento.CodigoMedicamento && dronMedicamento.CodigoDron == codigoDron)
                        {
                            foreach (var dron in dronModels.Where(c => c.NumeroSerie == codigoDron))
                            {
                                DronModels listaDronModels = new DronModels();
                                var peso = medicamento.Peso;
                                var bateria = (dron.CapacidadBateria / 100) * 100;
                                var Message = $"Numero serie: {dron.NumeroSerie} \nBateria: {dron.CapacidadBateria}%\nCapacidad de peso: {dron.PesoLimite}gr\nPeso utilizado: {peso}\nNombre del medicamento: {medicamento.Nombre}\nEstado: {dron.Estado}";
                                return Ok(Message);
                            }
                        }
                    }
               
                }
                //TODO:Falta implementar logica nivel bateria, peso, drones disponibles 
            }

            return DronMedicamento;
        }

        // PUT: api/DronMedicamento/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{codigoDron}")]
        public async Task<IActionResult> PutDronMedicamento(string codigoDron, DronMedicamento DronMedicamento)
        {
            List<DronModels> dronModels = await _context.DronModels.ToListAsync();

            if (codigoDron != DronMedicamento.CodigoDron)
            {
                return BadRequest();
            }

            _context.Entry(DronMedicamento).State = EntityState.Modified;

            try
            {
                if (DronMedicamento.CodigoDron == codigoDron)
                {
                    foreach (var dron in dronModels.Where(c => c.NumeroSerie == codigoDron))
                    {
                        if (Math.Round(dron.CapacidadBateria) <= 25)
                        {
                            return NotFound($"El dron no esta operativo, ya que su nivel de bateria es muy bajo: {Math.Round(dron.CapacidadBateria)}");
                        }
                        else
                        {
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DronMedicamentoExists(DronMedicamento.Id))
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
