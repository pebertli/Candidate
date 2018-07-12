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
    [Route("api/Profiles")]
    public class ProfilesController : Controller
    {
        private readonly DBContext _context;

        public ProfilesController(DBContext context)
        {
            _context = context;
            //if(_context.Profile.Count() == 0)
            //{
            //    _context.Profile.Add(new Profile
            //    {
            //        Id = 0,
            //        Bio = "Político",
            //        Name = "Bolsonaro"
            //    });
                //_context.SaveChanges();
            //}

        }

        // GET: api/Profiles
        [HttpGet]
        public IEnumerable<Profile> GetProfiles()
        {
            return _context.Profiles;
        }

        [HttpGet("{dto}")]
        public IEnumerable<Profile> GetProfiles([FromRoute] DateTimeOffset dto)
        {
            if (dto == null)
                return _context.Profiles;
            else
                return _context.Profiles.Where(p => p.LastUpdate >= dto || (p.Deleted != null && p.Deleted > dto));
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile([FromRoute] long id, [FromBody] Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profile.Id)
            {
                return BadRequest();
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profiles
        [HttpPost]
        public async Task<IActionResult> PostProfile([FromBody] Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return Ok(profile);
        }

        private bool ProfileExists(long id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}