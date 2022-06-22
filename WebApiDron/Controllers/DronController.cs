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
    public class DronController : ControllerBase
    {
        private readonly WebApiDronContext _context;

        public DronController(WebApiDronContext context)
        {
            _context = context;
        }

        // GET: api/Dron
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DronModels>>> GetDronModels()
        {
            return await _context.DronModels.ToListAsync();
        }

        // GET: api/Dron/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DronModels>> GetDronModels(int id)
        {

            var dronModels = await _context.DronModels.FindAsync(id);

            if (dronModels == null)
            {
                return NotFound();
            }

            return dronModels;
        }

        // PUT: api/Dron/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDronModels(string id, DronModels dronModels)
        {
            if (id != dronModels.NumeroSerie)
            {
                return BadRequest();
            }

            _context.Entry(dronModels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DronModelsExists(id))
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

        /// <summary>
        ///El peso limite del dron no puede ser mayor a 500.
        ///El numero de serie no puede ser mayor a 100 caracteres.
        ///El estado solo debe contener caracteres en mayusculas .
        /// </summary>
        /// <returns></returns>
        // POST: api/Dron
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DronModels>> PostDronModels(DronModels dronModels)
        {
            Regex regexEstado = new Regex("^[A-Z- ]+$");
            if (dronModels.PesoLimite > 500)
            {
                return NotFound("EL peso limite no puede ser mayor a 500.");
            }
            else if (dronModels.NumeroSerie.Length > 100)
            {
                return NotFound("El numero de serie no puede ser mayor a 100 caracteres.");
            }
             if (regexEstado.IsMatch(dronModels.Estado))
            {
                return Ok("Correcto");
            }
            else
            {
                return NotFound("El estado solo debe contener caracteres en mayusculas .");
            }
            //else if (dronModels.NumeroSerie == )
            //{
            //    return NotFound("El numero de serie ya existe.");
            //}
            //TODO: Falta validar el numero de serie, si existe en la BD o no.

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDronModels", new { id = dronModels.NumeroSerie }, dronModels);
        }

        // DELETE: api/Dron/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DronModels>> DeleteDronModels(int id)
        {
            var dronModels = await _context.DronModels.FindAsync(id);
            if (dronModels == null)
            {
                return NotFound();
            }

            _context.DronModels.Remove(dronModels);
            await _context.SaveChangesAsync();

            return dronModels;
        }

        private bool DronModelsExists(string id)
        {
            return _context.DronModels.Any(e => e.NumeroSerie == id);
        }
    }
}
