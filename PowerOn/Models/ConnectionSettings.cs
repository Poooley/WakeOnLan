namespace WakeOnLan.Models;

public class ConnectionSettings
{
    public List<WANConnectionSettings> WAN { get; set; }
    public List<LANConnectionSettings> LAN { get; set; }
}