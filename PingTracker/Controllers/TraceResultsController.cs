using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PingTracker.Data;
using PingTracker.Models;
using PingTracker.Server;

namespace PingTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraceResultsController : ControllerBase
    {
        private readonly PingTrackerContext _context;

        public TraceResultsController(PingTrackerContext context)
        {
            _context = context;
        }
        [HttpGet("bywebsite/{websiteId}")]
        public async Task<ActionResult<IEnumerable<TraceResult>>> GetResultsByWebsiteId(int websiteId)
        {
            if (websiteId <= 0) return BadRequest();
            var traceResults = await _context.TraceResults.Where(x => x.websiteId == websiteId).ToListAsync();
            if (traceResults.Count == 0 || traceResults == null) return NotFound();
            return Ok(traceResults);
        }
        [HttpGet("bytraceID/{id}")]
        public async Task<ActionResult<TraceResult>> GetResultsByID(int id) 
        {
            if (id <= 0) return BadRequest();
            var traceResult = await _context.TraceResults.Include(tr => tr.traceLines).Include(tr => tr.website).FirstOrDefaultAsync(tr => tr.id == id);
            if (traceResult == null) return NotFound();
            return Ok(traceResult);
        }
        // POSTS
        [HttpPost("Trace/{websiteId}")]
        public async Task<ActionResult<TraceResult>> GetTraceRoute(int websiteId)
        {
            if(websiteId <= 0) return BadRequest();
            var website = await _context.Websites.SingleOrDefaultAsync(site => site.Id == websiteId);
            if (website == null) return NotFound();
            TraceResult traceResult = PingTrack.TraceRoute(website.URL).Result;
            traceResult.websiteId = websiteId;
            _context.TraceResults.Add(traceResult);
            await _context.SaveChangesAsync();
            return Ok(traceResult);
        }
    }

}