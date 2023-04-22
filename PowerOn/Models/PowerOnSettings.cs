namespace WakeOnLan.Models;

public class PowerOnSettings
{
    public const string Section = "PowerOn";
    public string FileName { get; set; } = "executions.json";
    public int LocalUdpPort { get; set; }
    public int RetryAttempts { get; set; }
    public int RetryDelayMs { get; set; }
}