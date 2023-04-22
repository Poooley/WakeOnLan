using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using WakeOnLan.Exceptions;
using WakeOnLan.Models;

namespace WakeOnLan.Services;
public class WakeHelper
{
    public async Task Wake(LANConnectionSettings lan, PowerOnSettings settings)
    {
        var magicPacket = BuildMagicPacket(lan.MacAddress);

        await Wake(IPAddress.Parse(lan.IpAddress), magicPacket, settings.RetryAttempts, settings.RetryDelayMs,  settings.LocalUdpPort);
    }

    public async Task Wake(WANConnectionSettings wan, PowerOnSettings settings)
    {
        var magicPacket = BuildMagicPacket(wan.MacAddress);

        var routerIps = await Dns.GetHostAddressesAsync(wan.Domain);
        var routerIp = routerIps.FirstOrDefault();

        if (routerIp is null)
            throw new PowerOnException("Could not resolve router IP address");

        await Wake(routerIp, magicPacket, settings.RetryAttempts, settings.RetryDelayMs, wan.Port);
    }

    private static byte[] BuildMagicPacket(string macAddress)
    {
        macAddress = Regex.Replace(macAddress, "[: -]", "");
        var macBytes = Convert.FromHexString(macAddress);

        var header = Enumerable.Repeat((byte)0xff, 6);
        var data = Enumerable.Repeat(macBytes, 16).SelectMany(m => m);
        return header.Concat(data).ToArray();
    }

    private static async Task Wake(IPAddress ipAddress, byte[] magicPacket, int attempts, int retryDelay, int port)
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
        ProtocolType.Udp);

        var endPoint = new IPEndPoint(ipAddress, port);

        for (var i = 0; i < attempts; i++)
        {
            Console.WriteLine($"Try [{i} of {attempts}] Sending magic packet to {ipAddress}");
            socket.SendTo(magicPacket, endPoint);
            await Task.Delay(retryDelay);
        }
    }
}
