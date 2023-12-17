namespace PingTracker.DTO
{
    public class AddWebsiteDto
    {
        public string URL { get; set; } = string.Empty;
        public string? WebsiteNickname { get; set; }
        public int UserId { get; set; }
    }
}
