using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingTracker.Models
{
    public class Website
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string URL { get; set; } = string.Empty;
        [Column(TypeName ="decimal(5,4)")]
        public decimal? AveragePing { get; set; }
    }
}
