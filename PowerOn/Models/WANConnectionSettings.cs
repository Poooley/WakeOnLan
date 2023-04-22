using System.ComponentModel.DataAnnotations;

namespace WakeOnLan.Models;

public class WANConnectionSettings
{
    [Required]
    public string Domain { get; set; }
    
    [Required]
    [Range(1, 65535)]
    public int Port { get; set; }
    
    [Required]
    [RegularExpression(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", ErrorMessage = "The field MacAddress must be a valid MAC address.")]
    public string MacAddress { get; set; }
}