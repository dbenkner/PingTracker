namespace PingTracker.DTO
{
    public class SendPingDto
    {
        public string URL { get; set; } = string.Empty;
        public int WebsiteId { get; set; }
        public int UserId { get; set; }
    }
}
