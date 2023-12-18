using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace PingTracker.Models
{
    public class PingResult
    {
        public int Id { get; set; }
        public long RTT {  get; set; }
        [StringLength(40)]
        public string Status { get; set; } = string.Empty;
        public int WebsiteId { get; set; }
        [ForeignKey("WebsiteId")]
        public virtual Website? Website { get; set; }
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
    }
}
