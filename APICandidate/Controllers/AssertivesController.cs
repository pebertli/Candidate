using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICandidate.Model;

namespace APICandidate.Controllers
{
    [Produces("application/json")]
    [Route("api/Assertives")]
    public class AssertivesController : Controller
    {
        private readonly DBContext _context;

        public AssertivesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Assertives
        [HttpGet]
        public IEnumerable<Assertive> GetAssertives()
        {
            return _context.Assertives;
        }

        // GET: api/Assertives/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssertive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assertive = await _context.Assertives.SingleOrDefaultAsync(m => m.Id == id);

            if (assertive == null)
            {
                return NotFound();
            }

            return Ok(assertive);
        }

        // PUT: api/Assertives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssertive([FromRoute] int id, [FromBody] Assertive assertive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assertive.Id)
            {
                return BadRequest();
            }

            _context.Entry(assertive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssertiveExists(id))
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

        // POST: api/Assertives
        [HttpPost]
        public async Task<IActionResult> PostAssertive([FromBody] Assertive assertive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Assertives.Add(assertive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssertive", new { id = assertive.Id }, assertive);
        }

        // DELETE: api/Assertives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssertive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assertive = await _context.Assertives.SingleOrDefaultAsync(m => m.Id == id);
            if (assertive == null)
            {
                return NotFound();
            }

            _context.Assertives.Remove(assertive);
            await _context.SaveChangesAsync();

            return Ok(assertive);
        }

        private bool AssertiveExists(int id)
        {
            return _context.Assertives.Any(e => e.Id == id);
        }
    }
}