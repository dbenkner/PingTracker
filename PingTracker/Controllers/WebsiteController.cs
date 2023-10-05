using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PingTracker.Data;
using PingTracker.Models;

namespace PingTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly PingTrackerContext _context;

        public WebsiteController(PingTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Websites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Website>>> GetWebsites()
        {
          if (_context.Websites == null)
          {
              return NotFound();
          }
            return await _context.Websites.ToListAsync();
        }

        // GET: api/Websites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Website>> GetWebsites(int id)
        {
          if (_context.Websites == null)
          {
              return NotFound();
          }
            var websites = await _context.Websites.FindAsync(id);

            if (websites == null)
            {
                return NotFound();
            }

            return websites;
        }

        // PUT: api/Websites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebsites(int id, Website websites)
        {
            if (id != websites.Id)
            {
                return BadRequest();
            }

            _context.Entry(websites).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebsitesExists(id))
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

        // POST: api/Websites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Website>> PostWebsites(Website websites)
        {
          if (_context.Websites == null)
          {
              return Problem("Entity set 'PingTrackerContext.Websites'  is null.");
          }
            _context.Websites.Add(websites);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWebsites", new { id = websites.Id }, websites);
        }

        // DELETE: api/Websites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsites(int id)
        {
            if (_context.Websites == null)
            {
                return NotFound();
            }
            var websites = await _context.Websites.FindAsync(id);
            if (websites == null)
            {
                return NotFound();
            }

            _context.Websites.Remove(websites);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebsitesExists(int id)
        {
            return (_context.Websites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
