using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class MedicamentoModelsController : ControllerBase
    {
        private readonly WebApiDronContext _context;

        public MedicamentoModelsController(WebApiDronContext context)
        {
            _context = context;
        }

        // GET: api/MedicamentoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentoModels>>> GetMedicamentoModels()
        {
            return await _context.MedicamentoModels.ToListAsync();
        }

        // GET: api/MedicamentoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentoModels>> GetMedicamentoModels(int id)
        {
            var medicamentoModels = await _context.MedicamentoModels.FindAsync(id);

            if (medicamentoModels == null)
            {
                return NotFound();
            }

            return medicamentoModels;
        }

        // PUT: api/MedicamentoModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamentoModels(int id, MedicamentoModels medicamentoModels)
        {
            if (id != medicamentoModels.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicamentoModels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoModelsExists(id))
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

        // POST: api/MedicamentoModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MedicamentoModels>> PostMedicamentoModels(MedicamentoModels medicamentoModels)
        {
            Regex regexNombre= new Regex("^[a-zA-Z0-9_-]+$");
            Regex regexCodigo = new Regex("^[A-Z0-9_]+$");
            //string respuesta= medicamentoModels.Nombre.Substring(0);
            if (regexNombre.IsMatch(medicamentoModels.Nombre)) 
            {
                  Console.WriteLine("Correcto");
            }
            else
            {
                return NotFound("El nombre solo puede contener letras, numeros, guion y guion bajo");
                //Terminamos la aplicacion
            }
            if (regexCodigo.IsMatch(medicamentoModels.Codigo))
            {
                Console.WriteLine("Correcto");
            }
            else
            {
                return NotFound("El codigo solo puede contener Mayusculas, numeros y guion bajo.");
                //Terminamos la aplicacion
            }

            //Comienzo para añadir a la tabla "medicamentoModels".
            _context.MedicamentoModels.Add(medicamentoModels);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicamentoModels", new { id = medicamentoModels.Id }, medicamentoModels);
        }

        // DELETE: api/MedicamentoModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicamentoModels>> DeleteMedicamentoModels(int id)
        {
            var medicamentoModels = await _context.MedicamentoModels.FindAsync(id);
            if (medicamentoModels == null)
            {
                return NotFound();
            }

            _context.MedicamentoModels.Remove(medicamentoModels);
            await _context.SaveChangesAsync();

            return medicamentoModels;
        }

        private bool MedicamentoModelsExists(int id)
        {
            return _context.MedicamentoModels.Any(e => e.Id == id);
        }
    }
}
