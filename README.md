# PowerOn

PowerOn is a tool that sends Wake-on-LAN (WoL) packets to devices on a network, allowing you to remotely power on devices with a supported network interface. It can be configured to send packets to devices on both Wide Area Networks (WAN) and Local Area Networks (LAN).

## Getting Started

To get started with PowerOn, follow these steps:

1. Use git clone
2. Navigate to the WakeOnLan directory and restore the dependencies by running the following command in the project directory:
```shell
cd WakeOnLan
dotnet restore
```

3. Build the project using the following command:
```shell
dotnet build -c Release
```
4. Create/Adjust the appsettings.json and executions.json files and modify the settings according to your preferences.

5. Navigate to the PowerOn directory and run the application using the following command:
```shell
cd PowerOn
dotnet run -c Release
```

## Configuration
To configure PowerOn, you need to create and modify two JSON files: appsettings.json and executions.json. These files should be placed in the same folder as the built executable.

### appsettings.json
This file contains general settings for PowerOn, such as retry attempts, retry delay, and local UDP port. Here's an example:

```json
{
  "PowerOn": {
    "RetryAttempts": 6,
    "RetryDelayMs": 300,
    "LocalUdpPort": 9
  }
}
```

* RetryAttempts: The number of times to retry sending a WoL packet if the initial attempt fails.
* RetryDelayMs: The delay in milliseconds between retries.
* LocalUdpPort: (ONLY LAN) The destination port to send the WoL packet to.

### executions.json
This file contains the device configurations for both WAN and LAN devices. Here's an example:

```json
{
  "WAN": [
    {
      "Domain": "example.com",
      "Port": 13234,
      "MacAddress": "XX:XX:XX:XX:XX:XX"
    },
    {
      "Domain": "example.com",
      "Port": 13234,
      "MacAddress": "XX:XX:XX:XX:XX:XX"
    }
  ],
  "LAN": [
    {
      "IpAddress": "192.168.178.02",
      "MacAddress": "XX:XX:XX:XX:XX:XX"
    }
  ]
}
```
* WAN: An array of WAN device configurations, with each object containing the following properties:
  * Domain: The domain name or IP address of the device.
  * Port: The port to send the WoL packet to.
  * MacAddress: The MAC address of the device's network interface.
* LAN: An array of LAN device configurations, with each object containing the following properties:
  * IpAddress: The IP address of the device.
  * MacAddress: The MAC address of the device's network interface.
