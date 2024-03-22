using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace PingTracker.Models
{
    public class TraceResult
    {
        public int id { get; set; }
        public DateTime dateTimeOfTrace { get; set; } = DateTime.Now;
        [StringLength(100)]
        public string status {  get; set; } = string.Empty;
        public int websiteId { get; set; }
        public bool isComplete { get; set; } = false;
        public virtual Website? website { get; set; }
        
        public virtual List<TraceLine>? traceLines { get; set; } = new List<TraceLine>();
    }
}
