using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPICore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public FornecedoresController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<FornecedoresController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> Get()
        {
            return Ok(await _context.Fornecedores.ToListAsync());
        }

        // GET api/<FornecedoresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> GetAsync(Guid id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if (fornecedor == null) return NotFound();

            return Ok(fornecedor);
        }

        // POST api/<FornecedoresController>
        [HttpPost]
        public async Task<ActionResult> Post(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            return Created("", fornecedor);
        }

        // PUT api/<FornecedoresController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, Fornecedor fornecedor)
        {
            if(id != fornecedor.Id)
            {
                return BadRequest();
            }

            _context.Entry(fornecedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Fornecedores.Any(e => e.Id == id))
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

        // DELETE api/<FornecedoresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
