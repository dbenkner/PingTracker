using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingTracker.Models
{
    public class Website
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string URL { get; set; } = string.Empty;
        [Column(TypeName ="decimal(10,3)")]
        public decimal? AveragePing { get; set; }
        
    }
}
