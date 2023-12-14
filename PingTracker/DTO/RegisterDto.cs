using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PingTracker.DTO
{
    public class RegisterDto
    {
        [Required]
        [JsonProperty("username")]
        public string? Username { get; set; }
        [Required]
        [JsonProperty("email")]
        public string? Email { get; set; }
        [Required]
        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
