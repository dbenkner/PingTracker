using Microsoft.AspNetCore.Mvc;
using PingTracker.Models;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;


namespace PingTracker.Server
{
    public class PingTrack
    {
        private const string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        
        public static async Task<PingResult> MakePing(string url)
        {
            Ping pinger = new Ping();
            PingOptions pingerOptions = new PingOptions();
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 200;
            try
            {
                PingReply rep = await pinger.SendPingAsync(url, timeout, buffer, pingerOptions);
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
        public static async Task<PingResult> MakePing(string url, int ttl)
        {
            Ping pinger = new Ping();
            PingOptions pingOptions = new PingOptions(ttl, true);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 200;
            PingReply reply = await pinger.SendPingAsync(url, timeout, buffer, pingOptions);
            PingResult res = new PingResult() 
            { 
                 Address = reply.Address.ToString(),
                 Status = reply.Status.ToString(),
                 RTT = reply.RoundtripTime,
                 DateTime = DateTime.Now
            };
            return res;
        }
        public static async Task<TraceResult> TraceRoute(string url)
        {
            Stopwatch sw = Stopwatch.StartNew();
            TraceResult tr = new TraceResult();
            try
            {
                IPAddress ipAddress = Dns.GetHostEntry(url).AddressList[0];
            }
            catch (Exception) {
                bool valid = ValidateIPv4(url);
                if (!valid)
                {
                    tr.status = "invalid";
                    return tr;
                }
            }
            for (int i = 1; i < 30; i++)
            {
                TraceLine traceLine = new TraceLine();
                bool success = false;
                long[]? ipArr = new long[3];
                for (int j = 0; j < 3; j++) {
                    sw.Reset();
                    sw.Start();
                    PingResult pr = await MakePing(url, i);
                    ipArr[j] = pr.RTT;
                    traceLine.ip = pr.Address.ToString();
                    if (pr.Status == "Success")
                    {
                        success = true;
                    }
                }
                traceLine.ping1 = ipArr[0];
                traceLine.ping2 = ipArr[1];
                traceLine.ping3 = ipArr[2];
                traceLine.hop = i;
                tr.traceLines.Add(traceLine);
                if (success)
                {
                    tr.isComplete = true;
                    break;
                }
            }
            return tr;
        }
        public static bool ValidateIPv4(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            string[] urlArr = url.Split(".");
            if(urlArr.Length != 4) return false;    
            foreach(var sub in  urlArr)
            {
                byte oct;
                bool validIP = byte.TryParse(sub, out oct);
                if(!validIP) return false;
            }
            return true;
        }
    }
}
