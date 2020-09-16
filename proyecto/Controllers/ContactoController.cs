using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proyecto.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace proyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : ControllerBase
    {
        private readonly ProyectoContext _context;

        public ContactoController(ProyectoContext contexto)
        {
            _context=contexto;
        }

        [HttpGet]
        public List<Contacto> Get()
        {
            // Si tenemos gps voy a Base de datos y me traigo los objetos geocalibracion
            List<Contacto> listaContactos =_context.Contactos.ToList();
            return listaContactos;
        }

        [HttpGet("{id}")]
        public List<Contacto> Get(long id)
        {
            // Si tenemos gps voy a Base de datos y me traigo los objetos geocalibracion
            List<Contacto> listaContactos =  _context.Contactos.Where(x =>x.id ==id).ToList();
            return listaContactos;
        }

        [HttpGet("async/{id}")]
        [Authorize]

        public async Task<ActionResult<Contacto>> GetAsync(long id)
        {
            // Si tenemos gps voy a Base de datos y me traigo los objetos geocalibracion
            var contacto =  await _context.Contactos.FindAsync(id);
            if(contacto==null){
                return NotFound();
            }else{
                return contacto;
            }
        }
        [Authorize]
        [HttpPost]                                      //viene del cuerpo
        public async Task<ActionResult<Contacto>> Create([FromBody] Contacto item)
        {
            if(item==null){
                return BadRequest();
            }

            var currentUser = HttpContext.User;
            int years = 0;

            if (currentUser.HasClaim(c => c.Type == "FechaCreado"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "FechaCreado").Value);
                years = DateTime.Today.Year - date.Year;
            }

            if (years < 2)
            {
                return Forbid( );
            }

            _context.Contactos.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new {id = item.id}, item);
        }

        //actualizacion completa(no parcial)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id,[FromBody] Contacto item){
            if(item == null ||id==0){
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var contacto= await _context.Contactos.FindAsync(id);
            if(contacto==null){
                return NotFound();

            }
            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
