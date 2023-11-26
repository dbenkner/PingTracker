using Microsoft.AspNetCore.Mvc;
using PingTracker.Models;
using System.Net;
using System.Net.NetworkInformation;


namespace PingTracker.Server
{
    public class PingTrack
    {

        public static async Task<PingResult> MakePing(string url)
        {
            Ping pinger = new Ping();
            try
            {
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
            catch (Exception PingException)
            {
                Console.WriteLine(PingException.ToString(), PingException.Message);
                return new PingResult { Status = "HOST COULD NOT BE FOUND"
                };
            }
        }
    }
}
