using Microsoft.AspNetCore.Mvc;
using PingTracker.Models;
using System.Net.NetworkInformation;


namespace PingTracker.Server
{
    public class PingTrack
    {
        public static async Task<PingResult> MakePing(string url)
        {
            Ping pinger = new Ping();
            PingReply rep = await pinger.SendPingAsync(url);
            PingResult res = new PingResult
            {
                Address = rep.Address.ToString(),
                Status = rep.Status.ToString(),
                RTT = rep.RoundtripTime,
                DateTime = DateTime.Now,
            };
            return res;
        }
    }
}
