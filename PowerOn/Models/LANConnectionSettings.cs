using System.ComponentModel.DataAnnotations;

namespace WakeOnLan.Models;

public class LANConnectionSettings
{
    [Required]
    [RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b", ErrorMessage = "The field IpAddress must be a valid IPv4 address.")]
    public string IpAddress { get; set; }
    
    [Required]
    [RegularExpression(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", ErrorMessage = "The field MacAddress must be a valid MAC address.")]
    public string MacAddress { get; set; }
}