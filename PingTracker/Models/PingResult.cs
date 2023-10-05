using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace PingTracker.Models
{
    public class PingResult
    {
        public int Id { get; set; }
        public long RTT {  get; set; }
        public int WebsiteId { get; set; }
        public virtual List<Website>? Website { get; set; }
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        
    }
}
