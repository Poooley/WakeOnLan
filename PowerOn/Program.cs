using System.Reflection; 
using System.Text.Json;
using WakeOnLan.Services;
using WakeOnLan.Models;
using Microsoft.Extensions.Configuration;


const string EXEC_FILE = "executions.json";

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var appSettings = config.GetSection(PowerOnSettings.Section).Get<PowerOnSettings>();

var wakeHelper = new WakeHelper();

var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
Console.WriteLine($"Hello! Welcome to {assemblyName}!");

var connectionSettings = JsonSerializer.Deserialize<ConnectionSettings>(File.ReadAllText(EXEC_FILE));

if (!ValidateConnectionSettings(connectionSettings))
{
    Console.WriteLine("""

    Invalid connection settings. Please check your configuration.
    Please press any key to exit.

    """);
    Console.ReadKey();
    return;
}

if (connectionSettings.WAN is not null)
{
    Console.WriteLine("WAN connections:");
    foreach (var wanConnection in connectionSettings.WAN)
    {
        Console.WriteLine($"- Domain: {wanConnection.Domain}, Port: {wanConnection.Port}");
        await wakeHelper.Wake(wanConnection, appSettings);
    }
}

if (connectionSettings.LAN is not null)
{
    Console.WriteLine("LAN connections:");
    foreach (var lanConnection in connectionSettings.LAN ?? Enumerable.Empty<LANConnectionSettings >())
    {
        Console.WriteLine($"- IP Address: {lanConnection.IpAddress}, MAC Address: {lanConnection.MacAddress}");
        await wakeHelper.Wake(lanConnection, appSettings);
    }
}

Console.WriteLine("All systems are awake!");

await Task.Delay(2000);

static bool ValidateConnectionSettings(ConnectionSettings connectionSettings)
{
    var valid = true;

    foreach (var wanConnection in connectionSettings.WAN ?? Enumerable.Empty<WANConnectionSettings>())
    {
        if (!ModelValidator.TryValidate(wanConnection, out var wanValidationResults))
        {
            valid = false;
            Console.WriteLine($"Invalid WAN connection: Domain={wanConnection.Domain}, Port={wanConnection.Port}, MacAddress={wanConnection.MacAddress}");
            foreach (var validationResult in wanValidationResults)
            {
                Console.WriteLine($" - {validationResult.ErrorMessage}");
            }
        }
    }
    
    foreach (var lanConnection in connectionSettings.LAN ?? Enumerable.Empty<LANConnectionSettings>())
    {
        if (!ModelValidator.TryValidate(lanConnection, out var lanValidationResults))
        {
            valid = false;
            Console.WriteLine($"Invalid LAN connection: IpAddress={lanConnection.IpAddress}, MacAddress={lanConnection.MacAddress}");
            foreach (var validationResult in lanValidationResults)
            {
                Console.WriteLine($" - {validationResult.ErrorMessage}");
            }
        }
    }

    return valid;
}