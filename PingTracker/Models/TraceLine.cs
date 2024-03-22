using System.ComponentModel.DataAnnotations;

namespace PingTracker.Models
{
    public class TraceLine
    {
        public int id {  get; set; }
        [StringLength(255)]    
        public string ip { get; set; }
        public long? ping1 { get; set; }
        public long? ping2 { get; set; }
        public long? ping3 { get; set; }
        public int hop { get; set; }
        public int traceResultId { get; set; }
    }
}
