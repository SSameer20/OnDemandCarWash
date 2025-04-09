using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.DTO;

namespace Backend.Model;
public class Car{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Model { get; set; }= string.Empty;
    [Required]
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; }= string.Empty;

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();  
    public ICollection<WashOrder> WashOrders { get; set; } = new List<WashOrder>();
}