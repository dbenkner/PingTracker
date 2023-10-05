using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PingTracker.Data;
using PingTracker.Models;

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
        public async Task<ActionResult<PingResult>> PostPingResult(PingResult pingResult)
        {
          if (_context.PingResults == null)
          {
              return Problem("Entity set 'PingTrackerContext.PingResults'  is null.");
          }
            var PingWebste = await _context.Websites.Where(x => x.Id == pingResult.WebsiteId).SingleOrDefaultAsync();
            if (PingWebste == null) return BadRequest();
            PingReply result = SendPing(PingWebste.URL.ToString());
            pingResult.Address = result.Address.ToString();
            pingResult.RTT = result.RoundtripTime;
            
            _context.PingResults.Add(pingResult);
            await _context.SaveChangesAsync();

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
        private PingReply SendPing(string URL)
        {
            Ping pinger = new Ping();
            PingReply res = pinger.Send(URL);
            return res;
        }
        private async Task<IActionResult> UpdatePingAverage(int id, long rtt)
        {
            if (id != _context.Websites.Where(x => x.Id == id).FirstOrDefaultAsync().Id)
            {
                return NotFound();
            }
            var pings =  _context.PingResults.Where(x => x.WebsiteId == id).Select(x => x.RTT).ToList();
            pings.Add(rtt);
            var avg = pings.Average();
        }
    }
}
