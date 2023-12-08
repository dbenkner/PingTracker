using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PingTracker.Data;
using PingTracker.DTO;
using PingTracker.Models;
using PingTracker.Server;

namespace PingTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingResultsController : ControllerBase
    {
        private readonly PingTrackerContext _context;

        public PingResultsController(PingTrackerContext context)
        {
            _context = context;
        }

        // GET: api/PingResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PingResult>>> GetPingResults()
        {
          if (_context.PingResults == null)
          {
              return NotFound();
          }
            return await _context.PingResults.ToListAsync();
        }

        // GET: api/PingResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PingResult>> GetPingResult(int id)
        {
          if (_context.PingResults == null)
          {
              return NotFound();
          }
            var pingResult = await _context.PingResults.FindAsync(id);

            if (pingResult == null)
            {
                return NotFound();
            }

            return pingResult;
        }
        //GET: api/PingResults/website/
        [HttpGet("website/{id}")]
        public async Task<ActionResult<IEnumerable<PingResult>>> GetPingResultsByWebsiteID(int id)
        {
            if (_context.PingResults == null)
            {
                return NotFound();
            }
            var results = await _context.PingResults.Where(x => x.WebsiteId == id).Include(x => x.Website).ToArrayAsync();
            if (results.Count() == 0)
            {
                return BadRequest("No results found");
            }
            return results;
        }

        // PUT: api/PingResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPingResult(int id, PingResult pingResult)
        {
            if (id != pingResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(pingResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PingResultExists(id))
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

        // POST: api/PingResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PingResult>> PostPingResult(SendPingDto sendPingDto)
        {
            if (_context.PingResults == null)
            {
                return Problem("Entity set 'PingTrackerContext.PingResults'  is null.");
            }
            var PingWebste = await _context.Websites.Where(x => x.Id == sendPingDto.WebsiteId).SingleOrDefaultAsync();
            if (PingWebste == null) return BadRequest();

            PingResult pingResult = PingTrack.MakePing(sendPingDto.URL).Result;
            pingResult.WebsiteId = sendPingDto.WebsiteId;
            _context.PingResults.Add(pingResult);
            await _context.SaveChangesAsync();
            await UpdatePingAverage(pingResult.WebsiteId, pingResult.RTT);
            return CreatedAtAction("GetPingResult", new { id = pingResult.Id }, pingResult);
        }

        // DELETE: api/PingResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePingResult(int id)
        {
            if (_context.PingResults == null)
            {
                return NotFound();
            }
            var pingResult = await _context.PingResults.FindAsync(id);
            if (pingResult == null)
            {
                return NotFound();
            }

            _context.PingResults.Remove(pingResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PingResultExists(int id)
        {
            return (_context.PingResults?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task UpdatePingAverage(int id, long rtt)
        {
            decimal avg = Convert.ToDecimal(await _context.PingResults.Where(x => x.WebsiteId == id).AverageAsync(x => x.RTT));
            var sitePinged = await _context.Websites.FindAsync(id);
            sitePinged.AveragePing = avg;
            await _context.SaveChangesAsync();
        }
    }
}
