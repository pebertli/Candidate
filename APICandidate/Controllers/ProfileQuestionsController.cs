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
    [Route("api/ProfileQuestions")]
    public class ProfileQuestionsController : Controller
    {
        private readonly DBContext _context;

        public ProfileQuestionsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ProfileQuestions
        [HttpGet]
        public IEnumerable<ProfileQuestion> GetProfileQuestion()
        {
            return _context.ProfileQuestion.Include(pq => pq.Profile).
                Include(pq => pq.Question).ThenInclude(q => q.Assertives).OrderBy(x => x.Id);
        }

        // GET: api/ProfileQuestions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileQuestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileQuestion = await _context.ProfileQuestion.SingleOrDefaultAsync(m => m.Id == id);

            if (profileQuestion == null)
            {
                return NotFound();
            }

            return Ok(profileQuestion);
        }

        // PUT: api/ProfileQuestions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfileQuestion([FromRoute] int id, [FromBody] ProfileQuestion profileQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profileQuestion.Id)
            {
                return BadRequest();
            }

            _context.Entry(profileQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileQuestionExists(id))
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

        // POST: api/ProfileQuestions
        [HttpPost]
        public async Task<IActionResult> PostProfileQuestion([FromBody] ProfileQuestion profileQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProfileQuestion.Add(profileQuestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfileQuestion", new { id = profileQuestion.Id }, profileQuestion);
        }

        // DELETE: api/ProfileQuestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfileQuestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileQuestion = await _context.ProfileQuestion.SingleOrDefaultAsync(m => m.Id == id);
            if (profileQuestion == null)
            {
                return NotFound();
            }

            _context.ProfileQuestion.Remove(profileQuestion);
            await _context.SaveChangesAsync();

            return Ok(profileQuestion);
        }

        private bool ProfileQuestionExists(int id)
        {
            return _context.ProfileQuestion.Any(e => e.Id == id);
        }
    }
}